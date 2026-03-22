using EcommerceApi.Application.DTOs;
using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Application.Interfaces.Security;
using EcommerceApi.Application.Interfaces.Services;
using EcommerceApi.Application.Security;
using System.Linq;

namespace EcommerceApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUsuarioRepository usuarioRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
    {
        if(string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Contraseña))
            return null;

        var list = await _usuarioRepository.GetUsuarioByEmail(request.Email.Trim());
        var usuario = list.FirstOrDefault();
        if(usuario == null)
            return null;

        if(!PasswordHasher.Verify(request.Contraseña, usuario.Contraseña))
            return null;

        var jwt = _jwtTokenGenerator.CreateToken(usuario.Id, usuario.Email, usuario.Rol);

        return new LoginResponseDTO
        {
            Token = jwt.Token,
            ExpiraUtc = jwt.ExpiresUtc,
            Usuario = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            }
        };
    }
}
