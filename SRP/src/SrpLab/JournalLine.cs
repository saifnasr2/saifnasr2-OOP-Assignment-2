using System;

namespace SrpLab;

public class JournalLine
{
    private readonly SubscriptionPeriod _subscription;
    private readonly InvoiceManagement _invoice;

    public JournalLine(SubscriptionPeriod subscription, InvoiceManagement invoice)
    {
        _subscription = subscription;
        _invoice = invoice;
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        return $"{_subscription.CustomerId},{_invoice.NextInvoiceNumber()},{_subscription.Prorate(activeFrom):0.00}, AR-SUB";
    }
}
