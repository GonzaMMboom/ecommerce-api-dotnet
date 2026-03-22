using EcommerceApi.Application.Interfaces.Services;
using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Domain.Entities;
using EcommerceApi.Application.DTOs;
using EcommerceApi.Application.Exceptions;
using EcommerceApi.Application.Security;

namespace EcommerceApi.Application.Services;

public class UsuarioService : IUsuarioService{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository){
        _usuarioRepository = usuarioRepository;
    }

    private static UsuarioDTO ToDto(Usuario usuario) => new UsuarioDTO{
        Id = usuario.Id,
        Nombre = usuario.Nombre,
        Email = usuario.Email,
        Rol = usuario.Rol,
        Estado = usuario.Estado,
        FechaRegistro = usuario.FechaRegistro
    };

    public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id){
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if(usuario == null){
            throw new NotFoundException("Usuario no encontrado");
        }
        return ToDto(usuario);
    }

    public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync(){
        var usuarios = await _usuarioRepository.GetAllAsync();
        if(usuarios == null)
            return Enumerable.Empty<UsuarioDTO>();
        return usuarios.Select(ToDto);
    }

    public async Task<UsuarioDTO> CreateUsuarioAsync(UsuarioCreateDTO dto){
        if(string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("El email es requerido.");
        if(string.IsNullOrWhiteSpace(dto.Contraseña))
            throw new ArgumentException("La contrasena es requerida.");

        var existentes = await _usuarioRepository.GetUsuarioByEmail(dto.Email.Trim());
        if(existentes.Any())
            throw new ArgumentException("Ya existe un usuario con ese email.");

        var usuario = new Usuario{
            Nombre = dto.Nombre,
            Email = dto.Email.Trim(),
            Contraseña = PasswordHasher.Hash(dto.Contraseña),
            Rol = string.IsNullOrWhiteSpace(dto.Rol) ? "Usuario" : dto.Rol.Trim(),
            Estado = string.IsNullOrWhiteSpace(dto.Estado) ? "Activo" : dto.Estado.Trim(),
            FechaRegistro = DateTime.UtcNow
        };
        await _usuarioRepository.AddAsync(usuario);
        return ToDto(usuario);
    }
}
