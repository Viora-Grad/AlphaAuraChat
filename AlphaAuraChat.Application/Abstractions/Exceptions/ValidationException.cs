using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaAuraChat.Application.Abstractions.Exceptions;

public sealed class ValidationException : Exception
{

    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }
}
}
