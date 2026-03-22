using EcommerceApi.Application.DTOs;

namespace EcommerceApi.Application.Interfaces.Services;

public interface IUsuarioService 
{
    Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
    Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
    Task<UsuarioDTO> CreateUsuarioAsync(UsuarioCreateDTO usuarioCreateDTO);

}