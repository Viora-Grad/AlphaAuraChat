namespace AlphaAuraChat.Domain.Tenants;

public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid tenantId, CancellationToken cancellationToken);
}
