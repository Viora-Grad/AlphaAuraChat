using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Tenants.ChangeSubscriprion;

public sealed record ChangeSubscriptionCommand(Guid tenantId, Guid plandId) : ICommand;
