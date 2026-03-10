using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Plans;
using AlphaAuraChat.Domain.Tenants;

namespace AlphaAuraChat.Application.Tenant.RenewSubscription;

/// <summary>
/// Handles the renewal of a tenant subscription.
/// This operation extends the subscription expiration date
/// according to the provided renewal information.
/// </summary>

internal class RenewSubscriptionCommandHandler(
    IPlansRepository plansRepository,
    ITenantRepository tenantRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<RenewSubscriptionCommand>
{
    public async Task<Result> Handle(RenewSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var tenant = await tenantRepository.GetByIdAsync(request.tenantId, cancellationToken)
            ?? throw new NotFoundException($"this tenant with id {request.tenantId} not found");
        var plan = await plansRepository.GetByIdAsync(tenant.Subscription.PlanId)
            ?? throw new NotFoundException($"this plan with id {tenant.Subscription.PlanId} not found");
        tenant.RenewSubscription(plan.Duration.Value, dateTimeProvider.UtcNow);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
