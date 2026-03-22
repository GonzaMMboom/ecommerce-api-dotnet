using EcommerceApi.Application.Interfaces.Services;
using EcommerceApi.Application.Interfaces.Repositories;
using EcommerceApi.Domain.Entities;
using EcommerceApi.Application.DTOs;
using EcommerceApi.Application.Exceptions;

namespace EcommerceApi.Application.Services;

public class PedidoService : IPedidoService{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IProductoRepository _productoRepository;

    public PedidoService(
        IPedidoRepository pedidoRepository,
        IUsuarioRepository usuarioRepository,
        IProductoRepository productoRepository){
        _pedidoRepository = pedidoRepository;
        _usuarioRepository = usuarioRepository;
        _productoRepository = productoRepository;
    }
    public async Task<PedidoDTO> UsuarioCreatePedidoAsync(PedidoCreateDTO pedidoCreateDTO){
        var usuario = await _usuarioRepository.GetByIdAsync(pedidoCreateDTO.UsuarioId);
        if(usuario == null){
            throw new NotFoundException("Usuario no encontrado");
        }

        if(pedidoCreateDTO.PedidoDetalles == null || pedidoCreateDTO.PedidoDetalles.Count == 0){
            throw new ArgumentException("El pedido debe contener al menos un detalle (producto).");
        }

        // Estado permitido (según requerimiento del proyecto).
        var estadosPermitidos = new[] { "Nuevo", "En preparación", "Entregado" };
        var estadoEntrada = string.IsNullOrWhiteSpace(pedidoCreateDTO.Estado)
            ? "Nuevo"
            : pedidoCreateDTO.Estado.Trim();
        var estadoFinal = estadosPermitidos.FirstOrDefault(e => e.Equals(estadoEntrada, StringComparison.OrdinalIgnoreCase));
        if(estadoFinal == null){
            throw new ArgumentException("Estado de pedido inválido. Usa: Nuevo, En preparación o Entregado.");
        }

        // Validar y consolidar cantidades por producto (evita que el mismo producto aparezca duplicado y rompa el stock).
        var cantidadPorProducto = new Dictionary<int, int>();
        foreach(var detalle in pedidoCreateDTO.PedidoDetalles){
            if(detalle == null){
                throw new ArgumentException("Detalle de pedido inválido (detalle nulo).");
            }
            if(detalle.ProductoId <= 0){
                throw new ArgumentException("Cada detalle debe incluir un `ProductoId` válido.");
            }
            if(detalle.Cantidad <= 0){
                throw new ArgumentException("La `Cantidad` debe ser mayor que 0.");
            }

            cantidadPorProducto.TryGetValue(detalle.ProductoId, out var acumulado);
            cantidadPorProducto[detalle.ProductoId] = acumulado + detalle.Cantidad;
        }

        // Validar stock y construir detalles con precio actual del producto.
        var pedido = new Pedido{
            UsuarioId = usuario.Id,
            FechaPedido = pedidoCreateDTO.FechaPedido,
            Total = 0m, // se calcula abajo
            Estado = estadoFinal
        };

        var total = 0m;
        var productosParaActualizarStock = new Dictionary<int, (Producto Producto, int Cantidad)>();

        foreach(var (productoId, cantidad) in cantidadPorProducto){
            var producto = await _productoRepository.GetByIdAsync(productoId);
            if(producto == null){
                throw new NotFoundException($"Producto no encontrado: {productoId}");
            }
            if(producto.Stock < cantidad){
                throw new ArgumentException($"Stock insuficiente para el producto {productoId}. Disponible: {producto.Stock}, solicitado: {cantidad}.");
            }

            productosParaActualizarStock[productoId] = (producto, cantidad);

            var detalle = new PedidoDetalle{
                ProductoId = productoId,
                Cantidad = cantidad,
                PrecioUnitario = producto.Precio
            };
            pedido.PedidoDetalles.Add(detalle);
            total += producto.Precio * cantidad;
        }

        pedido.Total = total;

        await _pedidoRepository.AddAsync(pedido);

        // Actualizar stock del producto (opcional pero recomendado para evitar inconsistencias).
        foreach(var (productoId, info) in productosParaActualizarStock){
            info.Producto.Stock -= info.Cantidad;
            await _productoRepository.UpdateAsync(info.Producto);
        }

        return new PedidoDTO{
            Id = pedido.Id,
            UsuarioId = pedido.UsuarioId,
            FechaPedido = pedido.FechaPedido,
            Total = pedido.Total,
            Estado = pedido.Estado,
            Usuario = new UsuarioDTO{
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            },
            PedidoDetalles = pedido.PedidoDetalles.Select(d =>
            {
                productosParaActualizarStock.TryGetValue(d.ProductoId, out var info);
                return new PedidoDetalleDTO{
                    Id = d.Id,
                    PedidoId = d.PedidoId,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Producto = new ProductoDTO{
                        Id = d.ProductoId,
                        NombreProducto = info.Producto.NombreProducto,
                        Descripcion = info.Producto.Descripcion,
                        Precio = info.Producto.Precio,
                        Stock = info.Producto.Stock,
                        Estado = info.Producto.Estado
                    }
                };
            }).ToList()
        };
    }
}   