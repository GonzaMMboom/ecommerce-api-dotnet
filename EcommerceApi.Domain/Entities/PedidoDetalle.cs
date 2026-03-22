namespace EcommerceApi.Domain.Entities;

public class PedidoDetalle{
    public int Id { get; set; }
    public int Cantidad { get; set; } = 0;
    public decimal PrecioUnitario { get; set; } = 0;


    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;


    public int ProductoId { get; set; }
    public Producto? Producto { get; set; } = null!;

}