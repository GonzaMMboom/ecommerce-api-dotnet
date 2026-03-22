using EcommerceApi.Application.Security;

namespace EcommerceApi.Application.Interfaces.Security;

public interface IJwtTokenGenerator
{
    JwtTokenResult CreateToken(int userId, string email, string role);
}
