using System;

namespace SrpLab;

public class RequierdDocument
{
    private readonly LoanRiskCalculator _loanRisk;
    

    public RequierdDocument(LoanRiskCalculator loanRisk)
    {
        _loanRisk = loanRisk;
    }

    public IReadOnlyList <string> RequiredDocuments(bool isEligible)
    {
        var docs = new List<string> {"National ID", "Proof of income (3 months)"};
        if (_loanRisk.RequestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (_loanRisk.HasCollateral) docs.Add("Collateral ownership deed");
        if (_loanRisk.EmploymentMonths < 12) docs.Add("Employer letter");
        if (!isEligible) docs.Add("Manual underwriter referral form");
        return docs;
    }
}
