using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants;

namespace AlphaAuraChat.Application.Tenant.ActivateTenant;

internal sealed class ActivateTenantCommandHandler(
    ITenantRepository tenantRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ActivateTenantCommand>
{
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.tenantId, cancellationToken);
        if (tenant is null)
        {
            throw new NotFoundException($"this tenant with id{request.tenantId} not found ");
        }
        tenant.Activate();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
