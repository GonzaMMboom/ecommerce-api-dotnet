namespace EcommerceApi.Domain.Entities;

public class Usuario{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public  DateTime FechaRegistro { get; set; } = DateTime.Now;

    public ICollection<Pedido> Pedidos { get; set; }
}
