using EcommerceApi.Application.DTOs;

namespace EcommerceApi.Application.DTOs;

public class PedidoCreateDTO
{
    public decimal Total { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaPedido { get; set; } = DateTime.Now;
    public int UsuarioId { get; set; }
    public List<PedidoDetalleDTO> PedidoDetalles { get; set; } = new List<PedidoDetalleDTO>();
}
