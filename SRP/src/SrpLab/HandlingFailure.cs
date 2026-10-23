using System;

namespace SrpLab;

public class HandlingFailure
{
    private readonly SubscriptionPeriod _subscription;
    private readonly InvoiceManagement _invoice;
    public int FailedPayments {get; private set; }

    public HandlingFailure(SubscriptionPeriod subscription, InvoiceManagement invoice)
    {
        _subscription = subscription;
        _invoice = invoice;
    }

    public void RegisterFailedPayment() => FailedPayments++;

    public string DunningEmail(string customerName, DateOnly asOf)
    {
        var amount = _subscription.Prorate(_subscription.PeriodStart);
        var invoice = _invoice.NextInvoiceNumber();
        var severity = FailedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspicion"
        };
        return $"Subject: {severity} {invoice}\nHi {customerName}, \nBalance {amount:C} as of {asOf:o} ({FailedPayments} failures).\n";
    }
}
