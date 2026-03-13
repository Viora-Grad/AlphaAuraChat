using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Users;

namespace AlphaAuraChat.Application.Users.ChangeTenantMemberRole;

public class ChangeTenantMemberCommandHandler(
    ITenantMemberRepository tenantMemberRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ChangeTenantMemberRoleCommand>
{
    public async Task<Result> Handle(ChangeTenantMemberRoleCommand request, CancellationToken cancellationToken)
    {
        var tenantMember = await tenantMemberRepository.GetByIdAsync(request.id, cancellationToken)
            ?? throw new NotFoundException($"Tenant member with id {request.id} not found.");
        var result = tenantMember.ChangeRole(request.newRole);
        if (result.IsFailure)
            return Result.Failure(result.Error);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}
