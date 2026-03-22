namespace EcommerceApi.Application.Security;

public class JwtTokenResult
{
    public required string Token { get; init; }
    public DateTime ExpiresUtc { get; init; }
}
