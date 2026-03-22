using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Domain.Entities;
using EcommerceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Infrastructure.Repositories;

public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository{
    private readonly EcommerceDbContext _context;
    public PedidoRepository(EcommerceDbContext context) : base(context){
        _context = context;
    }
    public async Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAsync(int usuarioId){
        return await _context.Pedidos.Where(p => p.UsuarioId == usuarioId).ToListAsync();
    }
    public async Task<IEnumerable<Pedido>> GetPedidosByFechaPedidoAsync(DateTime fechaPedido){
        return await _context.Pedidos.Where(p => p.FechaPedido == fechaPedido).ToListAsync();
    }
    public async Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(string estado){
        return await _context.Pedidos.Where(p => p.Estado == estado).ToListAsync();
    }
    public async Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAndFechaPedidoAsync(int usuarioId, DateTime fechaPedido){
        return await _context.Pedidos.Where(p => p.UsuarioId == usuarioId && p.FechaPedido == fechaPedido).ToListAsync();
    }
    public async Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAndEstadoAsync(int usuarioId, string estado){
        return await _context.Pedidos.Where(p => p.UsuarioId == usuarioId && p.Estado == estado).ToListAsync();
    }
}