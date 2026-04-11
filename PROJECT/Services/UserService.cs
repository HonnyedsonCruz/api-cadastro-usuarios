using UserApi.DTOs;
using UserApi.Models;
using UserApi.Repositories;

namespace UserApi.Services;

public class UserService
{
    private readonly UserRepository _repository;

    public UserService(UserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserResponseDTO> CreateUser(CreateUserDTO dto)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash
        };

        var created = await _repository.Create(user);

        return new UserResponseDTO
        {
            Id = created.Id,
            Name = created.Name,
            Email = created.Email,
            CreatedAt = created.CreatedAt
        };
    }

    public async Task<List<UserResponseDTO>> GetAll()
    {
        var users = await _repository.GetAll();
        return users.Select(u => new UserResponseDTO
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            CreatedAt = u.CreatedAt
        }).ToList();
    }

    public async Task<UserResponseDTO?> GetById(int id)
    {
        var user = await _repository.GetById(id);
        if (user == null) return null;

        return new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<bool> Delete(int id) => await _repository.Delete(id);
}