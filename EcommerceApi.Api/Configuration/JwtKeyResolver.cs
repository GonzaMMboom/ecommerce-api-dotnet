namespace EcommerceApi.Api.Configuration;

internal static class JwtKeyResolver
{
    /// <summary>
    /// Orden: Jwt:Key en configuracion ya cargada, luego JWT_KEY o Jwt__Key en .env (ContentRoot), luego variables de entorno.
    /// </summary>
    internal static string? Resolve(WebApplicationBuilder builder)
    {
        var fromConfig = builder.Configuration["Jwt:Key"];
        if (!string.IsNullOrWhiteSpace(fromConfig))
            return fromConfig.Trim();

        var envPath = Path.Combine(builder.Environment.ContentRootPath, ".env");
        var fileVars = EnvFileLoader.Load(envPath);
        if (fileVars.TryGetValue("JWT_KEY", out var jwtKey) && !string.IsNullOrWhiteSpace(jwtKey))
            return jwtKey.Trim();
        if (fileVars.TryGetValue("Jwt__Key", out var jwtKeyAlt) && !string.IsNullOrWhiteSpace(jwtKeyAlt))
            return jwtKeyAlt.Trim();

        var fromEnv = Environment.GetEnvironmentVariable("JWT_KEY")
            ?? Environment.GetEnvironmentVariable("Jwt__Key");
        if (!string.IsNullOrWhiteSpace(fromEnv))
            return fromEnv.Trim();

        return null;
    }

    /// <summary>
    /// Asegura que IConfiguration exponga Jwt:Key para JwtTokenGenerator y el resto de la app.
    /// </summary>
    internal static string RequireAndApply(WebApplicationBuilder builder)
    {
        var jwtKey = Resolve(builder);
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException(
                "Falta la clave JWT. Define JWT_KEY en un archivo .env en la raiz del proyecto API (junto al .csproj), " +
                "o la variable de entorno JWT_KEY / Jwt__Key. Minimo unos 32 caracteres para HS256.");
        }

        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Jwt:Key"] = jwtKey
        });

        return jwtKey;
    }
}
