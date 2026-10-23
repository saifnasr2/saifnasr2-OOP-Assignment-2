using System;

namespace LibrarySystem;

public class Librarian : Staff
{
    public Librarian(string personId, string fullName, string phone, DateOnly hireDate, decimal monthlySalary)
        : base(personId, fullName, phone, hireDate, monthlySalary, 0m)
    {
    }

    public void ProcessReturn(Loan loan, DateOnly returnDate)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan));

        loan.ProcessReturn(returnDate);
    }

    public void MarkItemAsLost(Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan));

        loan.MarkAsLost();
    }
}
