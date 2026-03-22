namespace EcommerceApi.Api.Configuration;

/// <summary>
/// Lee un archivo .env con lineas KEY=valor (comentarios con #, valores entre comillas opcionales).
/// </summary>
internal static class EnvFileLoader
{
    internal static IReadOnlyDictionary<string, string> Load(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(path))
            return dict;

        foreach (var rawLine in File.ReadAllLines(path))
        {
            var line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith("#", StringComparison.Ordinal))
                continue;

            var eq = line.IndexOf('=');
            if (eq <= 0)
                continue;

            var key = line[..eq].Trim();
            var value = line[(eq + 1)..].Trim();
            if (value.Length >= 2)
            {
                if (value[0] == '"' && value[^1] == '"')
                    value = value[1..^1].Replace("\\\"", "\"", StringComparison.Ordinal);
                else if (value[0] == '\'' && value[^1] == '\'')
                    value = value[1..^1];
            }

            if (key.Length > 0)
                dict[key] = value;
        }

        return dict;
    }
}
