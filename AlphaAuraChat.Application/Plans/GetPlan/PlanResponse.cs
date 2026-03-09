
namespace AlphaAuraChat.Application.Plans.GetPlan;

public sealed class PlanResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Eula { get; init; } = null!;
    public int DurationInDays { get; init; }
    public decimal Price { get; init; }
    public string CurrencyCode { get; init; } = null!;
    public LimitationsResponse Limitations { get; init; } = null!;
}

public sealed class LimitationsResponse
{
    public int MaximumAdmins { get; init; }
    public int MaximumSupervisors { get; init; }
    public int MaximumAgents { get; init; }
    public int MaximumConversations { get; init; }
}