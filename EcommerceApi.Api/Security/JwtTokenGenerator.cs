using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceApi.Application.Interfaces.Security;
using EcommerceApi.Application.Security;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceApi.Api.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtTokenResult CreateToken(int userId, string email, string role)
    {
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key no esta configurado en appsettings.");
        var issuer = _configuration["Jwt:Issuer"] ?? "EcommerceApi";
        var audience = _configuration["Jwt:Audience"] ?? "EcommerceApiClients";
        var hours = int.TryParse(_configuration["Jwt:ExpireHours"], out var h) ? h : 8;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expiresUtc = DateTime.UtcNow.AddHours(hours);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, string.IsNullOrEmpty(role) ? "Usuario" : role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresUtc,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return new JwtTokenResult { Token = tokenString, ExpiresUtc = expiresUtc };
    }
}
