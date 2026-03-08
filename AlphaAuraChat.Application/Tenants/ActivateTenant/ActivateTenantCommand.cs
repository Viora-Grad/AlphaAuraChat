using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Tenants.ActivateTenant;

public record ActivateTenantCommand(Guid tenantId) : ICommand;
