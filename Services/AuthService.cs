using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserApi.DTOs;
using UserApi.Repositories;

namespace UserApi.Services;

public class AuthService
{
    private readonly UserRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(UserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<TokenResponseDTO?> Login(LoginDTO dto)
    {
        // Busca usuário pelo email
        var user = await _repository.GetByEmail(dto.Email);
        if (user == null) return null;

        // Verifica a senha
        var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!validPassword) return null;

        // Gera o token JWT
        var token = GenerateToken(user.Id, user.Name, user.Email);

        return new TokenResponseDTO
        {
            Token = token,
            Name = user.Name,
            Email = user.Email
        };
    }

    private string GenerateToken(int id, string name, string email)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}