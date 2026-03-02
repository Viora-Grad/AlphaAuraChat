namespace AlphaAuraChat.Domain.Users;

public sealed class Role
{
    //TODO seed in the infrastructure layer

    public static readonly Role Owner = new(1, "Owner");
    public static readonly Role Supervisor = new(2, "Supervisor");
    public static readonly Role Agent = new(3, "Agent");

    private Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; init; }
    public string Name { get; init; }
}