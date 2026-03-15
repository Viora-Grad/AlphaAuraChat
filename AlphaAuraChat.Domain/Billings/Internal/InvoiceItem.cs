using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Shared;

namespace AlphaAuraChat.Domain.Billings.Internal;

// there is a shadow property FK to Invoice in the database but we don't need to reference it in the domain model.
public class InvoiceItem : Entity
{
    public Name Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public Money Amount { get; private set; } = default!;
    public DateTime ProcessedAtUtc { get; private set; }

    private InvoiceItem(Name name, Description description, Money amount)
    {
        Name = name;
        Description = description;
        Amount = amount;
        ProcessedAtUtc = DateTime.UtcNow;
    }

    private InvoiceItem() { } // For EF Core

    public static Result<InvoiceItem> Create(Name name, Description description, Money money)
    {
        if (money.Amount <= 0)
            return Result.Failure<InvoiceItem>(InvoiceErrors.AmountMustBeGreaterThanZero);

        var item = new InvoiceItem(name, description, money);
        return Result.Success(item);
    }
}
