﻿namespace SemanticKernelMicroservice.Core.Application.Common.Exceptions;

public class CustomNotFoundException : Exception
{
    public CustomNotFoundException()
    {
    }

    public CustomNotFoundException(string message)
        : base(message)
    {
    }

    public CustomNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public CustomNotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}