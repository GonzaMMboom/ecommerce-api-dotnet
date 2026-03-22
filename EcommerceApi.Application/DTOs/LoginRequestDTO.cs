namespace EcommerceApi.Application.DTOs;

public class LoginRequestDTO
{
    public string Email { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
}
