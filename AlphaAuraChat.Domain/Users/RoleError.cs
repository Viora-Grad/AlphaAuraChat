using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Users;

public static class RoleError
{
    public static readonly Error roleNotFound = new("Role.NotFound", "The specified role does not exist.", ErrorCategory.NotFound);
}
