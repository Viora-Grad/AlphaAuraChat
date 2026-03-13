using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Users;
using AlphaAuraChat.Domain.Users.Internal;

namespace AlphaAuraChat.Application.Users.CreateTenantMember;

public sealed record CreateTenantMemberCommand(Guid TenantId, FirstName FirstName, LastName LastName, Role role) : ICommand<Guid>;
