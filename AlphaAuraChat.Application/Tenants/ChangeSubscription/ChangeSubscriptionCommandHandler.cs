using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Plans;
using AlphaAuraChat.Domain.Tenants;

namespace AlphaAuraChat.Application.Tenants.ChangeSubscriprion;

/// <summary>
/// Handles changing the subscription plan of a tenant.
/// This operation updates the tenant subscription
/// to a new plan based on the provided plan identifier.
/// </summary>

internal class ChangeSubscriptionCommandHandler(
    IPlansRepository planRepository,
    ITenantRepository tenantRepository,
    IUnitOfWork unitOfWork) :
    ICommandHandler<ChangeSubscriptionCommand>
{

    public async Task<Result> Handle(ChangeSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var tenant = await tenantRepository.GetByIdAsync(request.tenantId, cancellationToken)
            ?? throw new NotFoundException($"this tenant with id {request.tenantId} not found");
        var plan = await planRepository.GetByIdAsync(request.plandId)
            ?? throw new NotFoundException($"this plan with id {request.plandId} not found");
        var result = tenant.ChangeSubscription(request.plandId);
        if (result.IsFailure)
            return Result.Failure(result.Error);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
