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
/// <para>
/// Accessible users: AlphaAuraChat administrators. This handler is not exposed through any public API and is intended for internal use only, such as during initial seeding or administrative operations.<br></br>
/// <strong>Note: </strong>Currently returns the full Plan entity. May be changed to return only Guid in the future. 
/// </para>
/// </remarks>
/// <param name="plansRepository"> Repository for managing plan entities, used to check for existing similar plans and to add new plans.</param>
/// <param name="unitOfWork"> Unit of work for managing transactional operations, ensuring that all changes are committed atomically.</param>
/// 

internal sealed class CreatePlanCommandHandler(IPlansRepository plansRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreatePlanCommand, Plan>
{
    public async Task<Result<Plan>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var limitations = Limitations.Create(
            request.Limitations.MaximumAdmins,
            request.Limitations.MaximumSupervisors,
            request.Limitations.MaximumAgents,
            request.Limitations.MaximumConversations);

        if (limitations.IsFailure)
        {
            return Result.Failure<Plan>(limitations.Error); // might throw exceptions here instead but leave it untill validation is implemented 
        }

        var currency = Currency.FromCode(request.CurrencyCode);
        var price = new Money(request.Price, currency);
        if (await plansRepository.IsSimilarExists(limitations.Value, price))
            return Result.Failure<Plan>(PlanErrors.SimilarPlanExists);

        var plan = Plan.Create(
            new Name(request.Name),
            new Description(request.Description),
            new Eula(request.Eula),
            new Duration(TimeSpan.FromDays(request.DurationInDays)),
            price,
            limitations.Value);

        plansRepository.Add(plan);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // TODO: Consider returning only the Guid of the created plan instead of the full entity for better encapsulation and to avoid exposing unnecessary details.
        return Result.Success(plan);
    }





}
