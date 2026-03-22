using EcommerceApi.Application.Interfaces.Services;
using EcommerceApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsuarioController : ControllerBase{
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IUsuarioService usuarioService){
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioById(int id){
        try{
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            return Ok(usuario);
        }catch(NotFoundException){
            return NotFound();
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsuarios(){
        var usuarios = await _usuarioService.GetAllUsuariosAsync();
        return Ok(usuarios);
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUsuario([FromBody] UsuarioCreateDTO usuarioCreateDTO){
        if(usuarioCreateDTO == null)
            return BadRequest("El body es requerido.");
        try{
            var usuario = await _usuarioService.CreateUsuarioAsync(usuarioCreateDTO);
            return Ok(usuario);
        }catch(ArgumentException ex){
            return BadRequest(ex.Message);
        }
    }
}