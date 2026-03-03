using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Infrastructure.Repostories;

internal abstract class Repository<T>(ApplicationDbContext dbContext)
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    // consider adding another member to retrieve entities with out tracking for the read queries resulting in better memory management and performance

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }
}