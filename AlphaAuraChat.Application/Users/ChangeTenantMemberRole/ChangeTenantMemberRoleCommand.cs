using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Users;

namespace AlphaAuraChat.Application.Users.ChangeTenantMemberRole;

public sealed record ChangeTenantMemberRoleCommand(Guid id, Role newRole) : ICommand;

