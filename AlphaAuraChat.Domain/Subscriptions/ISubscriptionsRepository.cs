
namespace AlphaAuraChat.Domain.Subscriptions;

public interface ISubscriptionsRepository
{
    public void AddSubscription(Subscription subscription);

    public Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task<Subscription> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken);

    public Task<IEnumerable<Subscription>> GetAllAsync(CancellationToken cancellationToken);

    public Task<IEnumerable<Subscription>> GetByPlanIdAsync(Guid planId, CancellationToken cancellationToken);


}
