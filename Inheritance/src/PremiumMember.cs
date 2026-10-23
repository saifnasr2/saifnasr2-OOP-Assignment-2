namespace LibrarySystem;

public class PremiumMember : Member
{
    public int ReadingPoints
    {
        get
        {
            int returnedLoans = 0;
            foreach (Loan loan in Loans)
            {
                if (loan.Status == LoanStatus.Returned)
                    returnedLoans++;
            }

            return returnedLoans * 5;
        }
    }

    public PremiumMember(string personId, string fullName, string phone, decimal lateFeeDiscountPercent)
        : base(personId, fullName, phone, 10, lateFeeDiscountPercent)
    {
    }
}
