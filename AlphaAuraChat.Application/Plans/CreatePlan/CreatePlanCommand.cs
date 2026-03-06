using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Plans.CreatePlan;

public sealed record CreatePlanCommand(
    string Name,
    string Description,
    string Eula,
    int DurationInDays,
    decimal Price,
    LimitationsDto Limitations
    ) : ICommand;


public record LimitationsDto(
    int MaximumAdmins,
    int MaximumSupervisors,
    int MaximumAgents,
    int MaximumConversations);