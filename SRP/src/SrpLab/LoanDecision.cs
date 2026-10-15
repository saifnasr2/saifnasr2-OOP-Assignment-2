using System;

namespace SrpLab;

public class LoanDecision
{
    private readonly LoanRiskCalculator _loanRisk;
    private readonly RequierdDocument _required;

    public LoanDecision (LoanRiskCalculator loanRisk, RequierdDocument requierd)
    {
        _loanRisk = loanRisk;
        _required = requierd;
    }

    public bool IsEligible() => _loanRisk.RiskScore() >= 55m && _loanRisk.CreditScore >= 580;

    public string DecisionLetter(string applicantName)
    {
        if (IsEligible())
        {
            return $"Dear {applicantName},\nYour request for {_loanRisk.RequestedAmount:C} is pre-approved (risk {_loanRisk.RiskScore():0}).\n" +
                   $"Please upload: {string.Join("; ", _required.RequiredDocuments(IsEligible()))}.\n";
        }
        
        return $"Dear {applicantName},\nWe are unable to approve {_loanRisk.RequestedAmount:C} at this time.\n" +
               $"Reference risk={_loanRisk.RiskScore():0}. You may reapply after improving documentation.\n";

    }
}
