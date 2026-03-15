using AlphaAuraChat.Domain.Plans.Internal;
using AlphaAuraChat.Domain.Shared;

namespace AlphaAuraChat.Domain.Plans;

public interface IPlansRepository
{
    public void Add(Plan plan);
    public Task<Plan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Plan>> GetAllAsync(CancellationToken cancellationToken);
    public Task<bool> IsSimilarExists(Limitations limitations, Money price, Guid? excludePlanId = null); // checks if a similar plan (Limitations and price) already exists, excluding the current plan (for updates)
}
