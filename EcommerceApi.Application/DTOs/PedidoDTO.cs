using EcommerceApi.Application.DTOs;

namespace EcommerceApi.Application.DTOs;

public class PedidoDTO
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public UsuarioDTO Usuario { get; set; } = new UsuarioDTO();
    public DateTime FechaPedido { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public string Estado { get; set; } = string.Empty;
    public List<PedidoDetalleDTO> PedidoDetalles { get; set; } = new List<PedidoDetalleDTO>();
}