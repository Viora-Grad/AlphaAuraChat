using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants;

namespace AlphaAuraChat.Application.Tenants.ActivateTenant;

/// <summary>
/// Handles the activation of an existing tenant.
/// This operation updates the tenant status to active
/// allowing the tenant to access the system.
/// </summary>

internal sealed class ActivateTenantCommandHandler(
    ITenantRepository tenantRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ActivateTenantCommand>
{
    public async Task<Result> Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await tenantRepository.GetByIdAsync(request.tenantId, cancellationToken)
            ?? throw new NotFoundException($"Tenant with id {request.tenantId} not found");
        var result = tenant.Activate();
        if (result.IsFailure)
            return Result.Failure(result.Error);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
