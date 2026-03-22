namespace EcommerceApi.Application.Exceptions;

/// <summary>
/// Excepcion lanzada cuando un recurso no existe.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}

