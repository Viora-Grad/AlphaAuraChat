using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants;
using AlphaAuraChat.Domain.Users;

namespace AlphaAuraChat.Application.Users.CreateTenantMember;

public class CreateTenantCommandHandler(
    ITenantRepository tenantRepository,
    IUnitOfWork unitOfWork,
    ITenantMemberRepository tenantMemberRepository) : ICommandHandler<CreateTenantMemberCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTenantMemberCommand request, CancellationToken cancellationToken)
    {
        var tenant = tenantRepository.GetByIdAsync(request.TenantId, cancellationToken)
            ?? throw new NotFoundException($"Tenant with id {request.TenantId} not found.");
        var tenantMember = TenantMember.Create(request.FirstName, request.LastName, request.role, request.TenantId);
        tenantMemberRepository.Add(tenantMember);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(tenantMember.Id);
    }
}
