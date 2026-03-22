using EcommerceApi.Domain.Entities;

namespace EcommerceApi.Application.Interfaces.Repositories;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<IEnumerable<Usuario>> GetUsuarioByEmail(string email);
}