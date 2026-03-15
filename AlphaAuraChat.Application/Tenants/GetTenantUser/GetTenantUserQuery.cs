using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Tenants.GetTenantUser;

public sealed record GetTenantUserQuery(Guid TenantId) : IQuery<IEnumerable<UserResponse>>;
