using AlphaAuraChat.Domain.Users;
using FluentValidation;

namespace AlphaAuraChat.Application.Users.CreateTenantMember;

public class CreateTenantMemberCommandValidator : AbstractValidator<CreateTenantMemberCommand>
{
    public CreateTenantMemberCommandValidator()
    {
        RuleFor(x => x.role).Must(role => Role.CheckRole(role).IsSuccess).WithMessage("Invalid role. Valid roles are: Owner, Supervisor, Agent.");
    }
}
