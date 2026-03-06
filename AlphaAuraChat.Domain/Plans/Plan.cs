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
    public PlanStatus Status { get; private set; }


    private Plan(Guid id, Name name, Description description, Eula eula, Duration duration, Money price, Limitations limitations) : base(id)
    {
        Name = name;
        Description = description;
        Eula = eula;
        Duration = duration;
        Price = price;
        Limitations = limitations;
        Status = PlanStatus.Active;
    }

    private Plan() : base() { } // for EfCore

    public static Plan Create(Name name, Description description, Eula eula, Duration duration, Money price, Limitations limitations)
    {
        return new Plan(Guid.NewGuid(), name, description, eula, duration, price, limitations);
    }

    public Result Activate()
    {
        if (PlanStatus.Active == Status)
            return Result.Failure(PlanErrors.AlreadyActive);

        Status = PlanStatus.Active;
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (PlanStatus.Inactive == Status)
            return Result.Failure(PlanErrors.AlreadyInactive);

        Status = PlanStatus.Inactive;
        return Result.Success();
    }
    public Result Suspend()
    {
        if (PlanStatus.Suspended == Status)
            return Result.Failure(PlanErrors.AlreadySuspended);

        Status = PlanStatus.Suspended;
        return Result.Success();
    }
    public Result Cancel()
    {
        if (PlanStatus.Cancelled == Status)
            return Result.Failure(PlanErrors.AlreadyCancelled);

        Status = PlanStatus.Cancelled;
        return Result.Success();
    }
    // considering whether this belongs here or in a separate service, since it may involve more complex logic related to existing subscriptions, etc.
    public Result UpdateLimitations(Limitations newLimitations)
    {
        if (newLimitations == Limitations)
            return Result.Failure(PlanErrors.LimitationsMirrored);

        if (newLimitations.Exceeds(Limitations))
            return Result.Failure(PlanErrors.LimitationsExceeded);

        Limitations = newLimitations;
        return Result.Success();
    }


}
