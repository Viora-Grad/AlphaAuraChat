using AlphaAuraChat.Domain.Users;
using FluentValidation;

namespace AlphaAuraChat.Application.Users.ChangeTenantMemberRole;

public class ChangeTenantMemberRoleCommandValidator : AbstractValidator<ChangeTenantMemberRoleCommand>
{
    public ChangeTenantMemberRoleCommandValidator()
    {
        RuleFor(x => x.newRole)
            .Must(role => Role.CheckRole(role).IsSuccess).WithMessage("Invalid role. Valid roles are: Owner, Supervisor, Agent.");
    }
}
