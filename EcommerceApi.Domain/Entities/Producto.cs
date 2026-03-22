namespace EcommerceApi.Domain.Entities;

public class Producto{
    public int Id { get; set; }
    public string NombreProducto { get; set; } = string.Empty; //string.Empty es un valor por defecto para evitar nulls
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Estado { get; set; } = string.Empty;

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

}