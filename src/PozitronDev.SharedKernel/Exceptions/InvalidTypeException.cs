namespace PozitronDev.SharedKernel.Exceptions;

public class InvalidTypeException : AppException
{

    public InvalidTypeException(string message) : base(message)
    {
    }

    public InvalidTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
