using EcommerceApi.Application.DTOs;
using EcommerceApi.Domain.Entities;
using System.Linq.Expressions;

namespace EcommerceApi.Application.Interfaces.Services;

public interface IProductoService
{
    Task<ProductoDTO> GetProductoByIdAsync(int id);
    Task<IEnumerable<ProductoDTO>> GetAllProductosAsync();
    Task<ProductoDTO> CreateProductoAsync(ProductoDTO productoDTO);

}