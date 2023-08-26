﻿namespace PozitronDev.SharedKernel.Exceptions;

[Serializable]
public class InvalidTypeException : AppException
{

    public InvalidTypeException(string message) : base(message)
    {
    }

    public InvalidTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
    {
    }
}
