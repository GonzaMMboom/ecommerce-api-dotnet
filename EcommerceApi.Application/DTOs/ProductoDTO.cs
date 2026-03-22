namespace EcommerceApi.Application.DTOs;

public class ProductoDTO
{
    public int Id { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Estado { get; set; } = string.Empty;
}