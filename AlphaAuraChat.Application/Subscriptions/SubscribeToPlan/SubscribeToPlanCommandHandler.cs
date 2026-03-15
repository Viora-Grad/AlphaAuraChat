using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Billings;
using AlphaAuraChat.Domain.Billings.Internal;
using AlphaAuraChat.Domain.Plans;
using AlphaAuraChat.Domain.Shared;
using AlphaAuraChat.Domain.Subscriptions;
using AlphaAuraChat.Domain.Tenants;

namespace AlphaAuraChat.Application.Subscriptions.SubscribeToPlan;
/// <summary>
/// Command handler for subscribing a tenant to a plan. It creates a subscription for the tenant, generates an invoice for the subscription,
/// and saves both to the database as well as reflecting the change on the tenant instance.
/// The invoice is generated with the plan's price and a default expiry of 7 days.
/// For simplicity, we are assuming that there are no third party fees or service fees,
/// but this can be adjusted once we have more information about the fees structure from Paymob or any other payment provider we integrate with.<br/>
/// <remark><strong>Handle method returns the invoice instance created for that subscription</strong></remark>
/// </summary>


internal sealed class SubscribeToPlanCommandHandler(
    ITenantRepository tenantRepository,
    IPlansRepository plansRepository,
    ISubscriptionsRepository subscriptionsRepository,
    IBillingsRepository billingsRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<SubscribeToPlanCommand, Invoice>
{
    public async Task<Result<Invoice>> Handle(SubscribeToPlanCommand request, CancellationToken cancellationToken)
    {
        var tenant = await tenantRepository.GetByIdAsync(request.TenantId, cancellationToken) ??
            throw new NotFoundException($"Tenant with id {request.TenantId} not found.");

        var plan = await plansRepository.GetByIdAsync(request.PlanId, cancellationToken) ??
            throw new NotFoundException($"Plan with id {request.PlanId} not found.");

        var utcNow = dateTimeProvider.UtcNow;
        var expiryDate = utcNow.Add(plan.Duration.Value);
        var subscription = Subscription.Create(tenant.Id, plan.Id, utcNow, expiryDate);
        tenant.ChangeSubscription(subscription.Value.Id); // this can be in a domain event handler instead of direct method call (but for simplicity, we will keep it here)

        var itemName = new Name(plan.Name.Value);
        var itemDescription = new Description(plan.Description.Value);
        var itemAmount = plan.Price;
        var invoiceItem = InvoiceItem.Create(itemName, itemDescription, itemAmount);
        var thirdPartyFees = Money.Zero(Currency.Egp); // for simplicity, we will assume that there are no third party fees, till we know paymob fees structure
        var serviceFees = Money.Zero(Currency.Egp); // for simplicity, we will assume that there are no service fees, till we know the fees structure
        var invoiceExpirySpan = TimeSpan.FromDays(7); // defaulted to 7 days

        var invoice = Invoice.Create(tenant.Id, thirdPartyFees, serviceFees, invoiceExpirySpan, utcNow, new List<InvoiceItem> { invoiceItem.Value });

        subscriptionsRepository.Add(subscription.Value);
        billingsRepository.Add(invoice.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(invoice.Value);





    }
}
