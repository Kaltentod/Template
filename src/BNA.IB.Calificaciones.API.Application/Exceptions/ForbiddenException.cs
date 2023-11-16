namespace BNA.IB.Calificaciones.API.Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base()
    {
    }
    
    public ForbiddenException(string message)
        : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ForbiddenException(string name, object key)
        : base($"Forbidden operation for Entity \"{name}\" ({key}).")
    {
    }
}