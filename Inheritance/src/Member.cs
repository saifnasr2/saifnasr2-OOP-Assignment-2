using System;
using System.Collections.Generic;

namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new List<Loan>();

    public IReadOnlyList<Loan> Loans
    {
        get { return _loans.AsReadOnly(); }
    }

    public int MaximumActiveLoans { get; }
    public decimal LateFeeDiscountPercent { get; }

    protected Member(
        string personId,
        string fullName,
        string phone,
        int maximumActiveLoans,
        decimal lateFeeDiscountPercent)
        : base(personId, fullName, phone)
    {
        if (maximumActiveLoans <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumActiveLoans), "Maximum active loans must be positive.");
        if (lateFeeDiscountPercent < 0m || lateFeeDiscountPercent > 100m)
            throw new ArgumentOutOfRangeException(nameof(lateFeeDiscountPercent), "Discount must be between 0 and 100 percent.");

        MaximumActiveLoans = maximumActiveLoans;
        LateFeeDiscountPercent = lateFeeDiscountPercent;
    }

    public Loan BorrowItem(LibraryItem item, DateOnly borrowDate)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        int activeLoans = 0;
        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                activeLoans++;
        }

        if (activeLoans >= MaximumActiveLoans)
            throw new InvalidOperationException("This member has reached the active loan limit.");

        item.BeginLoan();
        Loan newLoan = new Loan(Guid.NewGuid().ToString("N"), this, item, borrowDate);
        _loans.Add(newLoan);
        return newLoan;
    }
}
