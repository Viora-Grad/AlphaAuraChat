namespace AlphaAuraChat.Application.Plans.GetAllPlans;
/// <summary>
/// A response model representing the details of a plan, including its limitations.
/// It includes properties for the plan's ID, name, description, EULA, duration, price, currency code, and limitations. 
/// The limitations are represented by a nested LimitationsResponse class that includes properties for the maximum number of admins, supervisors, agents, and conversations allowed under the plan.
/// <remark>
/// For now this response is a duplicate of the PlanResponse used in GetPlanQueryHandler,
/// it is created separately to prevent decoupling between the two queries, as they might evolve differently in the future.
/// </remark>
/// </summary>

public sealed class PlansResponse
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