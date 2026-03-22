namespace EcommerceApi.Domain.Entities;

public class Pedido{
    public int Id { get; set; }
    
    public int UsuarioId { get; set; }  //clave foranea de la tabla Usuario
    public Usuario Usuario { get; set; } = null!;   //esto representa la relacion de la tabla Pedido con la tabla Usuario. Aca se van a obtener los datos del usuario que realizo el pedido.

    public DateTime FechaPedido { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public string Estado { get; set; } = string.Empty;

    public ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

}