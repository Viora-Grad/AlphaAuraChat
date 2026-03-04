namespace AlphaAuraChat.Domain.Plans;

internal interface IPlansRepository
{
    public void Add(Plan plan);
    public Task<Plan?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Plan>> GetAllAsync();
}
