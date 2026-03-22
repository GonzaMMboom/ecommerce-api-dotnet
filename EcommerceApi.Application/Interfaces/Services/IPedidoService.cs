using EcommerceApi.Application.DTOs;
using EcommerceApi.Domain.Entities;
using System.Linq.Expressions;

namespace EcommerceApi.Application.Interfaces.Services;

public interface IPedidoService{
    Task<PedidoDTO> UsuarioCreatePedidoAsync(PedidoCreateDTO pedidoCreateDTO);
}