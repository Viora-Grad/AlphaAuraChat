using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Tenant.ActivateTenant;

public record ActivateTenantCommand(Guid tenantId) : ICommand;
