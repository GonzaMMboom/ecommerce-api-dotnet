using EcommerceApi.Application.Interfaces.Services;
using EcommerceApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductoController : ControllerBase{
    private readonly IProductoService _productoService;
    public ProductoController(IProductoService productoService){
        _productoService = productoService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductoById(int id){
        try{
            var producto = await _productoService.GetProductoByIdAsync(id);
            return Ok(producto);
        }catch(NotFoundException){
            return NotFound();
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProductos(){
        var productos = await _productoService.GetAllProductosAsync();
        return Ok(productos);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProducto(ProductoDTO productoDTO){
        var producto = await _productoService.CreateProductoAsync(productoDTO);
        return Ok(producto);
    }
}