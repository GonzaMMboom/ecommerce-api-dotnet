using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Domain.Entities;
using EcommerceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class{
    private readonly EcommerceDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(EcommerceDbContext context){
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id){
        return await _dbSet.FindAsync(id);
    }
    public async Task<IEnumerable<T>?> GetAllAsync(){
        return await _dbSet.ToListAsync();
    }
    public async Task<T> AddAsync(T entity){
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();   
        return entity;
    }
    public async Task<T> UpdateAsync(T entity){
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<T> DeleteAsync(int id){
        var entity = await GetByIdAsync(id);
        if(entity == null){
            throw new Exception("Entity not found");
        }
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}