namespace EcommerceApi.Application.DTOs;

public class LoginResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraUtc { get; set; }
    public UsuarioDTO Usuario { get; set; } = new();
}
