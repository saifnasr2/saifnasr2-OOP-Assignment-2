using System;

namespace SrpLab;

public class UnderwriterCsvExporter
{
    private readonly LoanRiskCalculator _loanRisk;
    private readonly LoanDecision _decision;

    public UnderwriterCsvExporter (LoanRiskCalculator loanRisk, LoanDecision decision)
    {
        _loanRisk = loanRisk;
        _decision = decision;
    }

    public string UnderwriterCsvRow(string applicantId)
    {
        
        return $"{applicantId},{_loanRisk.CreditScore},{_loanRisk.EmploymentMonths},{(_loanRisk.HasCollateral ? 1 : 0)},{_loanRisk.RiskScore():0.00},{(_decision.IsEligible() ? "Y" : "N")}";
    }
}
