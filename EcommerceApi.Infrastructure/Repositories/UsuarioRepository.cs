using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Domain.Entities;
using EcommerceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Infrastructure.Repositories;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository{
    private readonly EcommerceDbContext _context;
    public UsuarioRepository(EcommerceDbContext context) : base(context){
        _context = context;
    }
    public async Task<IEnumerable<Usuario>> GetUsuarioByEmail(string email){
        return await _context.Usuarios.Where(u => u.Email == email).ToListAsync();
    }
}