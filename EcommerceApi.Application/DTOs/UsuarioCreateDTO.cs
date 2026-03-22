namespace EcommerceApi.Application.DTOs;

/// <summary>
/// Datos para registrar un usuario (incluye contraseña; no se reutiliza en respuestas).
/// </summary>
public class UsuarioCreateDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
    public string Rol { get; set; } = "Usuario";
    public string Estado { get; set; } = "Activo";
}
