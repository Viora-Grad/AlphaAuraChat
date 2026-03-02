using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Shared;

namespace AlphaAuraChat.Domain.Billings.Internal;

public class InvoiceItem : Entity
{
    public Guid InvoiceId { get; private set; } = default!;
    public Name Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public Money Amount { get; private set; } = default!;
    public DateTime ProcessedAtUtc { get; private set; }

    private InvoiceItem(Guid invoiceId, Name name, Description description, Money amount)
    {
        InvoiceId = invoiceId;
        Name = name;
        Description = description;
        Amount = amount;
        ProcessedAtUtc = DateTime.UtcNow;
    }

    private InvoiceItem() { } // For EF Core

    public static Result<InvoiceItem> Create(Guid invoiceId, Name name, Description description, Money money)
    {
        if (money.Amount <= 0)
            return Result.Failure<InvoiceItem>(InvoiceErrors.AmountMustBeGreaterThanZero);

        var item = new InvoiceItem(invoiceId, name, description, money);
        return Result.Success(item);
    }
}
