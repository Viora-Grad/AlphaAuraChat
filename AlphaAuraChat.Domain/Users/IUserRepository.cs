namespace AlphaAuraChat.Domain.Users;

public interface IUserRepository
{
    public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<User>> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken);
}
