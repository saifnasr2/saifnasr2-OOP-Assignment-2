using System;

namespace LibrarySystem;

public class Staff : Person
{
    private readonly decimal _responsibilityAllowance;

    public DateOnly HireDate { get; }
    public decimal MonthlySalary { get; private set; }

    protected Staff(
        string personId,
        string fullName,
        string phone,
        DateOnly hireDate,
        decimal monthlySalary,
        decimal responsibilityAllowance)
        : base(personId, fullName, phone)
    {
        if (monthlySalary <= 0m)
            throw new ArgumentOutOfRangeException(nameof(monthlySalary), "Monthly salary must be positive.");
        if (responsibilityAllowance < 0m)
            throw new ArgumentOutOfRangeException(nameof(responsibilityAllowance), "Responsibility allowance cannot be negative.");

        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        _responsibilityAllowance = responsibilityAllowance;
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0m)
            throw new ArgumentOutOfRangeException(nameof(percentage), "Raise percentage must be greater than zero.");

        MonthlySalary = Math.Round(MonthlySalary + (MonthlySalary * percentage / 100m), 2);
    }

    public decimal GetMonthlyPay()
    {
        return MonthlySalary + _responsibilityAllowance;
    }
}
