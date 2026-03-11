# Billing Architecture Decision: Manual vs Recurring Billing

## Context
During the development of the `feat/Billings` branch, we evaluated two billing models for AlphaAuraChat's subscription system.

---

## Models Evaluated

### ❌ Option 1: Automatic Recurring Billing
- System auto-generates invoices on `NextBillingDate`.
- Requires storing client payment methods (PCI DSS compliance).
- Requires a background job to trigger charges.
- Requires dunning/retry logic for failed payments.

> **Verdict:** Not feasible right now — no payment infrastructure in place.

---

### ✅ Option 2: Manual (Client-Initiated) Renewal — **Selected**
- Client explicitly clicks "Renew."
- Invoice is generated on demand.
- Client pays via payment link.
- No card storage, no background jobs needed.

> **Verdict:** Simpler, ships faster, correct for current stage.

---

## Key Concepts Clarified

| Term | Meaning |
|------|---------|
| `Invoice.DueDateUtc` | Payment collection deadline, not subscription expiry |
| `Invoice.Overdue` | Payment not collected before due date |
| `Subscription.ExpiresAtUtc` | When client loses access |
| `Subscription.Expired` | Normal path in manual renewal; exceptional in auto-recurring |
| `Subscription.CancelledAtPeriodEnd` | Client cancelled but keeps access until `ExpiresAtUtc` |

---

## Cancellation Model

Decided on **cancel at period end** (industry standard):

- Client cancels → `Status = CancelledAtPeriodEnd`
- Access continues until `ExpiresAtUtc`
- No new invoice generated on renewal date
- No refund complexity

---

## Payment Gateway: Paymob *(future integration)*

### Why Paymob over Stripe

| Factor | Paymob | Stripe |
|--------|--------|--------|
| EGP support | ✅ Native | ⚠️ Limited |
| Egypt/MENA coverage | ✅ Yes | ⚠️ Partial |
| Local payments (Fawry, Vodafone Cash, ValU) | ✅ Yes | ❌ No |
| Developer experience | 🟡 Good | ✅ Excellent |

### Integration Flow *(when ready)*
Client subscribes        →  Paymob stores card token Paymob auto-charges      →  Webhook fired to your API

| Webhook Event | Action |
|---------------|--------|
| `payment_succeeded` | `Invoice.MarkAsPaid()` + extend `Subscription` |
| `payment_failed` | `Invoice.MarkAsOverdue()` |
| `subscription.cancelled` | `Subscription.Cancel()` |

### Benefits
- No card storage on your side
- No background charging job needed
- No PCI DSS liability
- Stripe-like developer experience for MENA

---

## Migration Path
Now   → Manual renewal (current implementation) Later → Add Paymob recurring (domain stays the same, just add webhooks)

> **Note:** Domain methods `MarkAsPaid()`, `MarkAsOverdue()`, and `Cancel()` require **no changes**
> when switching from manual to Paymob-driven recurring.
> Only the **trigger** changes: manual click → webhook event.

---

## Failed Payment Handling (Dunning)
- Paymob handles retries automatically.
- On final failure: `Invoice = Overdue` + `Subscription = Suspended`.
- Client has grace period to fix payment method.
- On recovery: `Invoice = Paid` + `Subscription = Active`.
- Not applicable in current manual renewal model

## Client Notifications
- Payment succeeded → email receipt to client.
- Payment failed → urgent email to update payment method.
- Subscription expiring soon → reminder email (3-7 days before).
- Implemented via Domain Events → notification handlers.

---

## Payment Gateway Integration: How Your App Gets Notified

### What is a Webhook?

A webhook is an HTTP POST request the payment gateway sends to your API automatically after a payment event occurs.


Client pays on Paymob → Paymob POSTs to https://yourapp.com/api/webhooks/paymob → Your API reacts

---

### End-to-End Payment Flow
1.	Client clicks "Pay" → App creates Invoice (Pending) → App requests payment page URL from Paymob → Client is redirected to Paymob's hosted payment page
2.	Client enters card details and confirms → Paymob processes the payment
3.	Payment result is determined on Paymob's side → Paymob sends POST to https://yourapp.com/api/webhooks/paymob → Payload contains event type and invoice reference
4.	Webhook endpoint receives the request → Validates Paymob signature (security check) → Dispatches MarkInvoicePaidCommand or MarkInvoiceOverdueCommand
5.	Command handler executes → Invoice.MarkAsPaid() or Invoice.MarkAsOverdue() → Domain event raised (InvoicePaymentSucceededDomainEvent)
6.	Domain event handler reacts → Subscription.Activate() or Subscription.Suspend()
7.	Done ✅

---

### Webhook Events Handled

| Paymob Event | Command Dispatched | Domain Event Raised | Subscription Result |
|---|---|---|---|
| `payment.success` | `MarkInvoicePaidCommand` | `InvoicePaymentSucceededDomainEvent` | `Activate()` |
| `payment.failed` | `MarkInvoiceOverdueCommand` | `InvoicePaymentFailedDomainEvent` | `Suspend()` |

---

### Security Considerations

- **Signature validation**: Every webhook request from Paymob is signed. Always verify the signature before processing.
- **Idempotency**: Paymob may send the same webhook more than once. Handlers must be safe to run multiple times without side effects.
- **Fast response**: Always return `200 OK` immediately. Never do heavy work before responding.

---

### Current State (Before Paymob Integration)

Until Paymob is integrated, `MarkInvoicePaidCommand` is triggered manually via an admin endpoint:
Admin marks invoice as paid → MarkInvoicePaidCommand → same flow as webhook


> **Note:** Only the trigger changes when Paymob is integrated.
> Commands, handlers, and domain events require **no modifications**.  
