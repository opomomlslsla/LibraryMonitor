namespace Domain.Exceptions;

public class NullEntityException : Exception
{
    public NullEntityException(string message) : base(message) { }

    public NullEntityException(string message, Exception innerException) : base(message, innerException) { }
}
