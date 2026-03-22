using EcommerceApi.Application.Interfaces.Services;
using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Domain.Entities;
using EcommerceApi.Application.DTOs;
using EcommerceApi.Application.Exceptions;

namespace EcommerceApi.Application.Services;

public class ProductoService : IProductoService{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository){
        _productoRepository = productoRepository;
    }

    public async Task<ProductoDTO> GetProductoByIdAsync(int id){
        var producto = await _productoRepository.GetByIdAsync(id);
        if(producto == null){
            throw new NotFoundException("Producto no encontrado");
        }
        return new ProductoDTO{
            Id = producto.Id,
            NombreProducto = producto.NombreProducto,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            Estado = producto.Estado
        };
    }
    public async Task<IEnumerable<ProductoDTO>> GetAllProductosAsync(){
        var productos = await _productoRepository.GetAllAsync();
        return productos.Select(producto => new ProductoDTO{
            Id = producto.Id,
            NombreProducto = producto.NombreProducto,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            Estado = producto.Estado
        });
    }
    public async Task<ProductoDTO> CreateProductoAsync(ProductoDTO productoDTO){
        var producto = new Producto{
            NombreProducto = productoDTO.NombreProducto,
            Descripcion = productoDTO.Descripcion,
            Precio = productoDTO.Precio,
            Stock = productoDTO.Stock,
            Estado = productoDTO.Estado
        };
        await _productoRepository.AddAsync(producto);
        return new ProductoDTO{
            Id = producto.Id,
            NombreProducto = producto.NombreProducto,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            Estado = producto.Estado
        };
    }
}