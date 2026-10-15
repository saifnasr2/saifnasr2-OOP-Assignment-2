using System;

namespace LibrarySystem;

public class LibraryItem
{
    private readonly decimal _lateFeeMultiplier;

    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriodDays { get; }
    public decimal BaseLateFee { get; private set; }
    public decimal DailyLateFee
    {
        get { return BaseLateFee * _lateFeeMultiplier; }
    }
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; private set; }

    protected LibraryItem(
        string catalogNumber,
        string title,
        int loanPeriodDays,
        decimal baseLateFee,
        decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException("Catalog number is required.", nameof(catalogNumber));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));
        if (loanPeriodDays <= 0)
            throw new ArgumentOutOfRangeException(nameof(loanPeriodDays), "Loan period must be positive.");
        if (baseLateFee <= 0m)
            throw new ArgumentOutOfRangeException(nameof(baseLateFee), "Base late fee must be greater than zero.");
        if (lateFeeMultiplier <= 0m)
            throw new ArgumentOutOfRangeException(nameof(lateFeeMultiplier), "Late-fee multiplier must be positive.");

        CatalogNumber = catalogNumber;
        Title = title;
        LoanPeriodDays = loanPeriodDays;
        BaseLateFee = baseLateFee;
        _lateFeeMultiplier = lateFeeMultiplier;
        IsWithdrawn = false;
        IsOnLoan = false;
    }

    internal void ChangeBaseLateFee(decimal newBaseLateFee)
    {
        if (newBaseLateFee <= 0m)
            throw new ArgumentOutOfRangeException(nameof(newBaseLateFee), "Base late fee must be greater than zero.");

        BaseLateFee = newBaseLateFee;
    }

    internal void WithdrawFromCirculation()
    {
        IsWithdrawn = true;
    }

    internal void RestoreToCirculation()
    {
        IsWithdrawn = false;
    }

    internal void BeginLoan()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException("A withdrawn item cannot be borrowed.");
        if (IsOnLoan)
            throw new InvalidOperationException("This item is already on loan.");

        IsOnLoan = true;
    }

    internal void CompleteReturn()
    {
        if (!IsOnLoan)
            throw new InvalidOperationException("This item is not currently on loan.");

        IsOnLoan = false;
    }
}
