using AlphaAuraChat.Application.Abstractions.Messaging;
using FluentValidation;
using MediatR;
using ValidationError = AlphaAuraChat.Application.Abstractions.Exceptions.ValidationError;
using ValidationException = AlphaAuraChat.Application.Abstractions.Exceptions.ValidationException;

namespace AlphaAuraChat.Application.Abstractions.Behaviors;

internal class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : ICommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        if (validationErrors.Count != 0)
        {
            throw new ValidationException(validationErrors);
        }

        return await next();
    }
}
