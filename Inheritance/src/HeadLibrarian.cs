using System;

namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    private const decimal ResponsibilityAllowance = 400m;

    public HeadLibrarian(string personId, string fullName, string phone, DateOnly hireDate, decimal monthlySalary)
        : base(personId, fullName, phone, hireDate, monthlySalary, ResponsibilityAllowance)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newBaseLateFee)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        item.ChangeBaseLateFee(newBaseLateFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        item.WithdrawFromCirculation();
    }

    public void RestoreItem(LibraryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        item.RestoreToCirculation();
    }
}
