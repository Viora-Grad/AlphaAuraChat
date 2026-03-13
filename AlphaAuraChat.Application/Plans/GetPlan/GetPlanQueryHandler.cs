using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Plans;


namespace AlphaAuraChat.Application.Plans.GetPlan;

public sealed class GetPlanQueryHandler(IPlansRepository plansRepository) : IQueryHandler<GetPlanQuery, PlanResponse>
{
    public async Task<Result<PlanResponse>> Handle(GetPlanQuery request, CancellationToken cancellationToken)
    {
        var plan = await plansRepository.GetByIdAsync(request.PlanId, cancellationToken) ??
            throw new NotFoundException($"Plan with ID {request.PlanId} not found.");

        var response = new PlanResponse()
        {
            Id = plan.Id,
            Name = plan.Name.Value,
            Description = plan.Description.Value,
            Eula = plan.Eula.Value,
            DurationInDays = plan.Duration.Value.Days,
            Price = plan.Price.Amount,
            CurrencyCode = plan.Price.Currency.Code,
            Limitations = new LimitationsResponse()
            {
                MaximumAdmins = plan.Limitations.MaximumAdmins,
                MaximumSupervisors = plan.Limitations.MaximumSupervisors,
                MaximumAgents = plan.Limitations.MaximumAgents,
                MaximumConversations = plan.Limitations.MaximumConversations
            }
        };

        return Result.Success(response);


    }
}
