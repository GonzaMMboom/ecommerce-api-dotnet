using EcommerceApi.Domain.Entities;

namespace EcommerceApi.Application.Interfaces.Repositories;

public interface IProductoRepository : IGenericRepository<Producto>
{
    Task<IEnumerable<Producto>> GetProductosByCategoriaIdAsync(int categoriaId);
}