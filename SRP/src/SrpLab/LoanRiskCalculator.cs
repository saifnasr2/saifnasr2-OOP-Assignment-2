using System;

namespace SrpLab;

public class LoanRiskCalculator
{
    private readonly RequierdDocument _document;
    public LoanDecision _decision;
    public decimal RequestedAmount {get; }
    public int CreditScore {get; } 
    public int EmploymentMonths {get; }
    public bool HasCollateral {get; }

    public LoanRiskCalculator(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
        _document = new RequierdDocument(this);
        _decision = new LoanDecision(this, _document);
    }

    public decimal RiskScore()
    {
        decimal score = 100m;
        score -= Math.Max(0, 700 - CreditScore) * 0.15m;
        if (EmploymentMonths < 6) score -= 20m;
        if (RequestedAmount > 50_000m && !HasCollateral) score -= 25m;
        if (RequestedAmount > 150_000m) score -= 10m;
        return Math.Clamp(score, 0m, 100m);
    }
}

