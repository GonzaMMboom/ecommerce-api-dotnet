using EcommerceApi.Domain.Entities;
using System.Linq.Expressions;

namespace EcommerceApi.Application.Interfaces.Repositories;

public interface IPedidoRepository : IGenericRepository<Pedido>{
    Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAsync(int usuarioId);
    Task<IEnumerable<Pedido>> GetPedidosByFechaPedidoAsync(DateTime fechaPedido);
    Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(string estado);
    Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAndFechaPedidoAsync(int usuarioId, DateTime fechaPedido);
    Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAndEstadoAsync(int usuarioId, string estado);
}