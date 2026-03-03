using AlphaAuraChat.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AlphaAuraChat.Infrastructure;

internal class ApplicationDbContext : DbContext, IUnitOfWork
{
    public override Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}

