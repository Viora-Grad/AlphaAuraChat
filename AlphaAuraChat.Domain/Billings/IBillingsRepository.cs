
namespace AlphaAuraChat.Domain.Billings;

public interface IBillingsRepository
{
    public void Add(Invoice invoice);
    public Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Invoice>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
