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

    public async Task<(UserResponseDTO? user, string? error)> CreateUser(CreateUserDTO dto)
    {
        var emailExists = await _repository.EmailExists(dto.Email);
        if (emailExists)
            return (null, "Este e-mail já está cadastrado.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash
        };

        var created = await _repository.Create(user);

        return (new UserResponseDTO
        {
            Id = created.Id,
            Name = created.Name,
            Email = created.Email,
            CreatedAt = created.CreatedAt
        }, null);
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
    public async Task<(UserResponseDTO? user, string? error)> Update(int id, UpdateUserDTO dto)
    {
        var emailExists = await _repository.EmailExistsForOtherUser(id, dto.Email);
        if (emailExists)
            return (null, "Este e-mail já está sendo usado por outro usuário.");

        var updated = await _repository.Update(id, dto.Name, dto.Email);
        if (updated == null)
            return (null, "Usuário não encontrado.");

        return (new UserResponseDTO
        {
            Id = updated.Id,
            Name = updated.Name,
            Email = updated.Email,
            CreatedAt = updated.CreatedAt
        }, null);
    }
}