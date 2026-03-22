using EcommerceApi.Application.DTOs;

namespace EcommerceApi.Application.DTOs;

public class PedidoDetalleDTO
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public PedidoDTO Pedido { get; set; } = new PedidoDTO();
    public int ProductoId { get; set; }
    public ProductoDTO Producto { get; set; } = new ProductoDTO();
    public int Cantidad { get; set; } = 0;
    public decimal PrecioUnitario { get; set; } = 0;
}