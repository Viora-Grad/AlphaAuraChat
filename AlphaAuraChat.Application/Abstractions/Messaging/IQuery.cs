using AlphaAuraChat.Domain.Abstractions;
using MediatR;

namespace AlphaAuraChat.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }