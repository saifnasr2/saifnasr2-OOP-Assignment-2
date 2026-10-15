using System;

namespace LibrarySystem;

public class Loan
{
    public string LoanId { get; }
    public DateOnly BorrowDate { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }
    public DateOnly DueDate
    {
        get { return BorrowDate.AddDays(Item.LoanPeriodDays); }
    }
    public LoanStatus Status { get; private set; }
    public DateOnly? ReturnDate { get; private set; }

    public decimal LateFee
    {
        get
        {
            if (!ReturnDate.HasValue)
                return 0m;

            int daysLate = ReturnDate.Value.DayNumber - DueDate.DayNumber;
            if (daysLate <= 0)
                return 0m;

            decimal feeBeforeDiscount = daysLate * Item.DailyLateFee;
            decimal discount = feeBeforeDiscount * Member.LateFeeDiscountPercent / 100m;
            return Math.Max(0m, feeBeforeDiscount - discount);
        }
    }

    internal Loan(string loanId, Member member, LibraryItem item, DateOnly borrowDate)
    {
        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID is required.", nameof(loanId));
        if (member == null)
            throw new ArgumentNullException(nameof(member));
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        LoanId = loanId;
        Member = member;
        Item = item;
        BorrowDate = borrowDate;
        Status = LoanStatus.Borrowed;
        ReturnDate = null;
    }

    internal void ProcessReturn(DateOnly returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be returned.");
        if (returnDate < BorrowDate)
            throw new ArgumentOutOfRangeException(nameof(returnDate), "Return date cannot be earlier than borrow date.");

        Item.CompleteReturn();
        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
    }

    internal void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be marked as lost.");

        Status = LoanStatus.Lost;
        // A lost item remains unavailable: IsOnLoan is not cleared as though it had been returned.
    }
}
