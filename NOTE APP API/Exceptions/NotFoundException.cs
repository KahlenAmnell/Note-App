﻿namespace Note_App_API.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

}