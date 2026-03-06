using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Plans;
using AlphaAuraChat.Domain.Plans.Internal;
using AlphaAuraChat.Domain.Shared;
namespace AlphaAuraChat.Application.Plans.CreatePlan;

/// <summary>
/// Handler for processing the creation of new subscription plans. This handler validates the input data, creates a new plan entity,
/// and persists it to the database.
/// </summary>
/// <remarks>
/// Accessible users: AlphaAuraChat administrators. This handler is not exposed through any public API and is intended for internal use only, such as during initial seeding or administrative operations.
/// </remarks>

internal sealed class CreatePlanCommandHandler(IPlansRepository plansRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreatePlanCommand>
{
    public async Task<Result> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var limitations = Limitations.Create(
            request.Limitations.MaximumAdmins,
            request.Limitations.MaximumSupervisors,
            request.Limitations.MaximumAgents,
            request.Limitations.MaximumConversations);

        if (limitations.IsFailure)
        {
            return Result.Failure(limitations.Error); // might throw exceptions here instead but leave it untill validation is implemented 
        }

        var price = new Money(request.Price, Currency.Egp);
        if (await plansRepository.IsSimilarExists(limitations.Value, price))
            return Result.Failure(PlanErrors.SimilarPlanExists);

        var plan = Plan.Create(
            new Name(request.Name),
            new Description(request.Description),
            new Eula(request.Eula),
            new Duration(TimeSpan.FromDays(request.DurationInDays)),
            price,
            limitations.Value);

        plansRepository.Add(plan);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }





}
