using AlphaAuraChat.Domain.Abstractions;
using MediatR;

namespace AlphaAuraChat.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }