using AlphaAuraChat.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AlphaAuraChat.Infrastructure.Repostories;

internal abstract class Repository<T>(ApplicationDbContext dbContext)
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    // consider adding another member to retrieve entities with out tracking for the read queries resulting in better memory management and performance

    #region query ops
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().FindAsync([id], cancellationToken);
    }
    #endregion  

    #region Addition ops
    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await DbContext.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task Add(T entity, CancellationToken cancellationToken)
    {
        await DbContext.AddAsync(entity, cancellationToken);
    }
    #endregion

    #region Deletion ops
    public void Remove(Guid id)
    {
        DbContext.Set<T>()
           .Where(entity => entity.Id == id)
           .ExecuteDelete();
    }

    public void Remove(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entites)
    {
        DbContext.Set<T>().RemoveRange(entites);
    }

    public void RemoveRange(IEnumerable<Guid> ids)
    {
        DbContext.Set<T>()
           .Where(entity => ids.Contains(entity.Id))
           .ExecuteDelete();
    }
    #endregion 
}