using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Plans.Internal;
using AlphaAuraChat.Domain.Shared;

namespace AlphaAuraChat.Domain.Plans;

/// <summary>
/// Represents a subscription plan, including its name, duration, and pricing details.
///
/// The seeding of the plans is handled in the AlphaAuraChat.Infrastructure project,
/// no external interface is provided for plan management.
/// </summary>
public sealed class Plan : Entity
{
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public Eula Eula { get; private set; } = null!;
    public Duration Duration { get; private set; } = null!;
    public Money Price { get; private set; } = null!;
    public Limitations Limitations { get; private set; } = null!;

    private Plan(Guid id, Name name, Description description, Eula eula, Duration duration, Money price, Limitations limitations) : base(id)
    {
        Name = name;
        Description = description;
        Eula = eula;
        Duration = duration;
        Price = price;
        Limitations = limitations;
    }

    private Plan() : base() { } // for EfCore
}
