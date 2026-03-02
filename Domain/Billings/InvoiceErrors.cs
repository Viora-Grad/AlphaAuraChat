using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Billings;

public static class InvoiceErrors
{
    public static Error PaidAfterExpiry => new("Invoice.PaidAfterExpiry", "Invoice date is expired.");
    public static Error AlreadyPaid => new("Invoice.AlreadyPaid", "Invoice is already paid.");
    public static Error OverdueAfterPayment => new("Invoice.OverdueAfterPayment", "Invoice is already paid, cannot be marked as overdue.");
    public static Error AmountMustBeGreaterThanZero => new("InvoiceItem.AmountMustBeGreaterThanZero", "Invoice item amount must be greater than zero.");

}
