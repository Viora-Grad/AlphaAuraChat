using AlphaAuraChat.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace AlphaAuraChat.Application.Abstractions.Behaviors;

internal class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{

    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        try
        {
            _logger.LogInformation("Handling {RequestName} with content: {@Request}", requestName, request);
            var result = await next();
            if (result.IsSuccess)
            {
                _logger.LogInformation("Handled {RequestName} successfully with result: {@Result}", requestName, result);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    _logger.LogError("Request {RequestName} processed with error", requestName);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while handling {RequestName}", requestName);
            throw;
        }
    }
}
