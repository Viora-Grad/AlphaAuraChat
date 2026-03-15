using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Billings.Internal;
using AlphaAuraChat.Domain.Shared;

namespace AlphaAuraChat.Domain.Billings;

public class Invoice : Entity
{
    public Guid ClientId { get; private set; } = default!; // ClientId references TenantId, as each tenant is a client in the billing system.
    public Guid? TransactionId { get; private set; } // TransactionId is set when the invoice is paid, referencing the payment transaction in the payment provider.

    public Money ThirdPartyFees { get; private set; } = default!;
    public Money ServiceFees { get; private set; } = default!;

    private readonly List<InvoiceItem> InvoiceItems = [];
    public IReadOnlyCollection<InvoiceItem> Items => InvoiceItems.AsReadOnly();

    public Money Total => InvoiceItems.Aggregate(Money.Zero(Total.Currency), (Total, item) => Total + item.Amount) + ThirdPartyFees + ServiceFees;

    public DateTime RequestedAtUtc { get; private set; }
    public DateTime? PaidAtUtc { get; private set; }
    public DateTime DueDateUtc { get; private set; }

    public InvoiceStatus Status { get; private set; }

    private Invoice(Guid id, Guid clientId, Money thirdPartyFees, Money serviceFees, DateTime requestedAtUtc) : base(id)
    {
        ClientId = clientId;
        ThirdPartyFees = thirdPartyFees;
        ServiceFees = serviceFees;
        RequestedAtUtc = requestedAtUtc;
    }

    private Invoice() { } // for EfCore

    public static Result<Invoice> Create(
        Guid clientId, Money thirdPartyFees,
        Money serviceFees,
        TimeSpan expirySpan,
        DateTime utcNow,
        ICollection<InvoiceItem> invoiceItems)
    {
        var result = new Invoice(Guid.NewGuid(), clientId, thirdPartyFees, serviceFees, utcNow)
        {
            DueDateUtc = utcNow + expirySpan,
            Status = InvoiceStatus.Pending
        };
        result.InvoiceItems.AddRange(invoiceItems);

        return Result.Success(result);
    }

    public Result MarkAsPaid()
    {
        if (InvoiceStatus.Overdue == Status)
            return Result.Failure(InvoiceErrors.PaidAfterExpiry);

        if (InvoiceStatus.Paid == Status)
            return Result.Failure(InvoiceErrors.AlreadyPaid);

        // TODO: Set TransactionId after integrating with the payment provider.
        PaidAtUtc = DateTime.UtcNow;
        Status = InvoiceStatus.Paid;

        return Result.Success();
    }

    public Result MarkAsOverdue()
    {
        if (Status == InvoiceStatus.Paid)
            return Result.Failure(InvoiceErrors.OverdueAfterPayment);

        Status = InvoiceStatus.Overdue;

        return Result.Success();
    }
}