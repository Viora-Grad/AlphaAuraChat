using AlphaAuraChat.Domain.Tenants.Internal;

namespace AlphaAuraChat.Application.Tenants.Dtos;

public class TenantDto
{
    public Guid Id { get; set; }
    public Name? Name { get; set; }
    public Contact? Contacts { get; set; }
    public Subscription? Subscription { get; set; }
}
