using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Domain.Entities;
using EcommerceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Infrastructure.Repositories;

public class ProductoRepository : GenericRepository<Producto>, IProductoRepository{     
    private readonly EcommerceDbContext _context;   
    public ProductoRepository(EcommerceDbContext context) : base(context){
        _context = context; //
    }
    public async Task<IEnumerable<Producto>> GetProductosByCategoriaIdAsync(int categoriaId){
        return await _context.Productos.Where(p => p.CategoriaId == categoriaId).ToListAsync();
    }
}