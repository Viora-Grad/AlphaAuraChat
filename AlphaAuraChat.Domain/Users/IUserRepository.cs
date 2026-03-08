namespace AlphaAuraChat.Domain.Users;

public interface IUserRepository
{
    public Task<User> GetByIdAsync(Guid id);
}
