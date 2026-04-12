namespace UserApi.DTOs;

// O que chega quando CRIA um usuário
public class CreateUserDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

// O que RETORNA para quem chamou a API (nunca expõe a senha)
public class UserResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
public class UpdateUserDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
public class LoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class TokenResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}