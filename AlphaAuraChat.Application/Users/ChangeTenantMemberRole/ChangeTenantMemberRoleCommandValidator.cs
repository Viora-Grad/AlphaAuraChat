using AlphaAuraChat.Domain.Users;
using FluentValidation;

namespace AlphaAuraChat.Application.Users.ChangeTenantMemberRole;

/// <summary>
/// Validates whether a given role is correct and recognized 
/// within the system. This ensures that only authorized and 
/// predefined roles are accepted, maintaining consistency and 
/// enforcing access control rules.
/// </summary>


public class ChangeTenantMemberRoleCommandValidator : AbstractValidator<ChangeTenantMemberRoleCommand>
{
    public ChangeTenantMemberRoleCommandValidator()
    {
        RuleFor(x => x.newRole)
            .Must(role => Role.CheckRole(role).IsSuccess).WithMessage("Invalid role. Valid roles are: Owner, Supervisor, Agent.");
    }
}
