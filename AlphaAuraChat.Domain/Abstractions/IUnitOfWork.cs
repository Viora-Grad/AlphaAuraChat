namespace AlphaAuraChat.Domain.Abstractions;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken token = default);
}
