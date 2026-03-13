using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Plans;

namespace AlphaAuraChat.Application.Plans.GetAllPlans;

/// <summary>
/// Handles the retrieval of all plans.This query does not take any parameters and returns a list of all available plans with their details. 
/// The results are cached to improve performance, as the list of plans is expected to change infrequently.
/// </summary>

public sealed class GetAllPlansQueryHandler(IPlansRepository planRepository) : IQueryHandler<GetAllPlansQuery, IReadOnlyList<PlansResponse>>
{
    public async Task<Result<IReadOnlyList<PlansResponse>>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await planRepository.GetAllAsync(cancellationToken);
        var response = plans.Select(plan => new PlansResponse
        {
            Id = plan.Id,
            Name = plan.Name.Value,
            Description = plan.Description.Value,
            Eula = plan.Eula.Value,
            DurationInDays = plan.Duration.Value.Days,
            Price = plan.Price.Amount,
            CurrencyCode = plan.Price.Currency.Code,
            Limitations = new LimitationsResponse
            {
                MaximumAdmins = plan.Limitations.MaximumAdmins,
                MaximumSupervisors = plan.Limitations.MaximumSupervisors,
                MaximumAgents = plan.Limitations.MaximumAgents,
                MaximumConversations = plan.Limitations.MaximumConversations
            }
        }).ToList();

        return Result.Success<IReadOnlyList<PlansResponse>>(response);
    }
}
