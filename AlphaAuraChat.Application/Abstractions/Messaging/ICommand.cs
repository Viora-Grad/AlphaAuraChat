using AlphaAuraChat.Domain.Abstractions;
using MediatR;

namespace AlphaAuraChat.Application.Abstractions.Messaging;

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand { }

public interface ICommand : IRequest<Result>, IBaseCommand { }

public interface IBaseCommand { }