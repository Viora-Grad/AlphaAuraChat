using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Users.Internal;

namespace AlphaAuraChat.Application.Users.CreateCustomer;

public sealed record CreateCustomerCommand(FirstName FirstName, LastName LastName, Guid TenantId, RelativeId RelativeId) : ICommand<Guid>;


