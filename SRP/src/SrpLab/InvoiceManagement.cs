using System;

namespace SrpLab;

public class InvoiceManagement
{
    private static int _invoiceSeq = 1000;

    private readonly SubscriptionPeriod _subscription;

    public InvoiceManagement(SubscriptionPeriod subscription)
    {
        _subscription = subscription;
    }

    public string NextInvoiceNumber()
    {
        var n = ++_invoiceSeq;
        return $"INV-{_subscription.PeriodStart:yyyyMM}-{n:D5}";
    }
}
