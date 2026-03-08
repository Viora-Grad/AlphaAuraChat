namespace AlphaAuraChat.Domain.Plans;

public interface IPlansRepository
{
    public void Add(Plan plan);
    public Task<Plan?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Plan>> GetAllAsync();
}
