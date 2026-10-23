using System;

namespace SrpLab;

public class SubscriptionPeriod
{
    private readonly InvoiceManagement _invoice;
    public HandlingFailure Handling {get; }
    public string CustomerId {get; }
    public decimal MonthlyPrice {get; }
    public DateOnly PeriodStart {get; }
    public DateOnly PeriodEnd {get; }

    public SubscriptionPeriod(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        _invoice = new InvoiceManagement(this);
        Handling = new HandlingFailure(this, _invoice);
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd; 
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        if (activeFrom <= PeriodStart) return MonthlyPrice;
        if (activeFrom >= PeriodEnd) return 0m;
        var totalDays = PeriodEnd.DayNumber - PeriodStart.DayNumber;
        if (totalDays <= 0) return MonthlyPrice;
        var used = PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(MonthlyPrice * used / totalDays, 2);
    }

}
