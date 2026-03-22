
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Domain.Entities;


namespace EcommerceApi.Infrastructure.Data;

public class EcommerceDbContext : DbContext{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options){
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
    }
}