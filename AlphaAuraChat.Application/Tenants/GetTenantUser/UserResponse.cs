using AlphaAuraChat.Domain.Users.Internal;

namespace AlphaAuraChat.Application.Tenants.GetTenantUser;

public class UserResponse
{
    public Guid Id { get; set; }
    public Guid TenantID { get; set; }
    public Guid RelativeId { get; set; }
    public FirstName? FirstName { get; set; }
    public LastName? LastName { get; set; }

}
