using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Billings;

public static class InvoiceErrors
{
    public static Error PaidAfterExpiry => new("Invoice.PaidAfterExpiry", "Invoice date is expired.", ErrorCategory.Conflict);
    public static Error AlreadyPaid => new("Invoice.AlreadyPaid", "Invoice is already paid.", ErrorCategory.Conflict);
    public static Error OverdueAfterPayment => new("Invoice.OverdueAfterPayment", "Invoice is already paid, cannot be marked as overdue.", ErrorCategory.Conflict);
    public static Error AmountMustBeGreaterThanZero => new("InvoiceItem.AmountMustBeGreaterThanZero", "Invoice item amount must be greater than zero.", ErrorCategory.Validation);

}
