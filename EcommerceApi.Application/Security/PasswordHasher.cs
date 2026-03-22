using System.Security.Cryptography;
using System.Text;

namespace EcommerceApi.Application.Security;

/// <summary>
/// Hash PBKDF2 para contrasenas (sin dependencias externas).
/// Formato almacenado: Base64(salt):Base64(hash)
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public static string Hash(string password)
    {
        if(string.IsNullOrEmpty(password))
            throw new ArgumentException("La contrasena es requerida.", nameof(password));

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            Algorithm,
            KeySize);
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public static bool Verify(string password, string storedHash)
    {
        if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
            return false;

        var parts = storedHash.Split(':', 2);
        if(parts.Length != 2)
            return false;

        try
        {
            var salt = Convert.FromBase64String(parts[0]);
            var expected = Convert.FromBase64String(parts[1]);
            var actual = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                Algorithm,
                expected.Length);
            return CryptographicOperations.FixedTimeEquals(expected, actual);
        }
        catch
        {
            return false;
        }
    }
}
