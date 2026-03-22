using EcommerceApi.Application.DTOs;

namespace EcommerceApi.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
}
