using EcommerceApi.Application.DTOs;
using EcommerceApi.Application.Exceptions;
using EcommerceApi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceApi.Api.Controllers;

[Route("api/pedidos")]
[ApiController]
[Authorize]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePedido([FromBody] PedidoCreateDTO pedidoCreateDTO)
    {
        if(pedidoCreateDTO == null)
        {
            return BadRequest("El body es requerido.");
        }

        try
        {
            var pedido = await _pedidoService.UsuarioCreatePedidoAsync(pedidoCreateDTO);
            return Ok(pedido);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

