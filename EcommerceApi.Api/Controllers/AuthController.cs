using EcommerceApi.Application.DTOs;
using EcommerceApi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        if(request == null)
            return BadRequest("El body es requerido.");

        var result = await _authService.LoginAsync(request);
        if(result == null)
            return Unauthorized("Email o contrasena incorrectos.");

        return Ok(result);
    }
}
