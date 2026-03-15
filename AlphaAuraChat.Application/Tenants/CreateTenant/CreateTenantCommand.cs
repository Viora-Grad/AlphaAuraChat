using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Tenants.Internal;
using AlphaAuraChat.Domain.Users;

namespace AlphaAuraChat.Application.Tenants.CreateTenant;

public record CreateTenantCommand(Name name, Contact contanct, TenantMember tenantOwner) : ICommand<Guid>;

