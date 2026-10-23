using System;
using System.Collections.Generic;
using LibrarySystem;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== LIBRARY SYSTEM: INHERITANCE DEMONSTRATION ===\n");

        StudentMember student = new StudentMember("M-001", "Mariam Ali", "01000000001");
        PremiumMember premium = new PremiumMember("M-002", "Omar Hassan", "01000000002", 10m);

        Book book = new Book("B-001", "Clean Code", 1.00m);
        DVD dvd = new DVD("D-001", "C# Training", 2.50m);
        Magazine magazine = new Magazine("MAG-001", "Science Monthly", 1.50m);


        Console.WriteLine("-- Staff polymorphism: monthly pay --");
        Librarian librarian = new Librarian("S-001", "Laila Librarian", "01000000101", new DateOnly(2018, 5, 1), 9000m);
        HeadLibrarian headLibrarian = new HeadLibrarian("S-002", "Hassan Head", "01000000102", new DateOnly(2012, 3, 15), 15000m);
        Shelver shelver = new Shelver("S-003", "Nour Shelver", "01000000103", new DateOnly(2022, 7, 1), 6500m, "Fiction");

        List<Staff> staffMembers = new List<Staff>();
        staffMembers.Add(librarian);
        staffMembers.Add(headLibrarian);
        staffMembers.Add(shelver);

        foreach (Staff staffMember in staffMembers)
        {
            Console.WriteLine(staffMember.FullName + ": " + staffMember.GetMonthlyPay().ToString("C2"));
        }

        Console.WriteLine("\n-- Library item polymorphism: loan periods and daily late fees --");
        List<LibraryItem> items = new List<LibraryItem>();
        items.Add(book);
        items.Add(dvd);
        items.Add(magazine);

        foreach (LibraryItem item in items)
        {
            Console.WriteLine(item.Title + ": loan period=" + item.LoanPeriodDays + " days, daily late fee=" + item.DailyLateFee.ToString("C2"));
        }

        Console.WriteLine("\n-- Raise and fee validation --");
        try
        {
            staffMembers[0].GiveRaise(0m);
        }
        catch (Exception ex)
        {
            WriteRejectedAction("A zero-percent raise", ex);
        }

        try
        {
            headLibrarian.ChangeLateFee(book, 0m);
        }
        catch (Exception ex)
        {
            WriteRejectedAction("A zero base late fee", ex);
        }

        Console.WriteLine("\n-- Withdrawn item cannot be borrowed --");
        Book withdrawnBook = new Book("B-002", "Withdrawn Book", 1.00m);
        headLibrarian.WithdrawItem(withdrawnBook);
        try
        {
            premium.BorrowItem(withdrawnBook, new DateOnly(2026, 10, 1));
        }
        catch (Exception ex)
        {
            WriteRejectedAction("Borrowing a withdrawn book", ex);
        }

        Console.WriteLine("\n-- An item cannot be borrowed twice --");
        Book sharedBook = new Book("B-003", "Shared Book", 1.00m);
        student.BorrowItem(sharedBook, new DateOnly(2026, 10, 1));
        try
        {
            premium.BorrowItem(sharedBook, new DateOnly(2026, 10, 1));
        }
        catch (Exception ex)
        {
            WriteRejectedAction("Borrowing a book already on loan", ex);
        }

        Console.WriteLine("\n-- A student cannot borrow a fourth active item --");
        Book studentBook2 = new Book("B-101", "Book Two", 1.00m);
        Book studentBook3 = new Book("B-102", "Book Three", 1.00m);
        Book studentBook4 = new Book("B-103", "Book Four", 1.00m);
        student.BorrowItem(studentBook2, new DateOnly(2026, 10, 1));
        student.BorrowItem(studentBook3, new DateOnly(2026, 10, 1));
        try
        {
            student.BorrowItem(studentBook4, new DateOnly(2026, 10, 1));
        }
        catch (Exception ex)
        {
            WriteRejectedAction("Borrowing beyond the student's three-item limit", ex);
        }

        Console.WriteLine("\n-- Premium member returns a DVD five days late --");
        Loan premiumLoan = premium.BorrowItem(dvd, new DateOnly(2026, 10, 1));
        Console.WriteLine("Due date: " + premiumLoan.DueDate);
        DateOnly fiveDaysLate = premiumLoan.DueDate.AddDays(5);
        librarian.ProcessReturn(premiumLoan, fiveDaysLate);
        Console.WriteLine("Return date: " + premiumLoan.ReturnDate);
        Console.WriteLine("Late fee after 10% premium discount: " + premiumLoan.LateFee.ToString("C2"));
        Console.WriteLine("Premium reading points: " + premium.ReadingPoints);

        Console.WriteLine("\n-- Invalid return date is rejected --");
        StudentMember dateTestMember = new StudentMember("M-003", "Date Test Member", "01000000003");
        Book dateTestBook = new Book("B-201", "Date Validation Book", 1.00m);
        Loan dateTestLoan = dateTestMember.BorrowItem(dateTestBook, new DateOnly(2026, 10, 10));
        try
        {
            librarian.ProcessReturn(dateTestLoan, new DateOnly(2026, 10, 9));
        }
        catch (Exception ex)
        {
            WriteRejectedAction("Returning an item before its borrow date", ex);
        }

        Console.WriteLine("\n-- Illegal status transitions are rejected --");
        try
        {
            librarian.ProcessReturn(premiumLoan, fiveDaysLate.AddDays(1));
        }
        catch (Exception ex)
        {
            WriteRejectedAction("Returning the same loan twice", ex);
        }

        try
        {
            librarian.MarkItemAsLost(premiumLoan);
        }
        catch (Exception ex)
        {
            WriteRejectedAction("Marking a returned loan as lost", ex);
        }

        Console.WriteLine("\n-- A borrowed loan can be marked as lost only once --");
        StudentMember lostTestMember = new StudentMember("M-004", "Lost Test Member", "01000000004");
        Book lostTestBook = new Book("B-301", "Lost Test Book", 1.00m);
        Loan lostTestLoan = lostTestMember.BorrowItem(lostTestBook, new DateOnly(2026, 10, 1));
        librarian.MarkItemAsLost(lostTestLoan);
        Console.WriteLine("Loan status after marking lost: " + lostTestLoan.Status);
        try
        {
            librarian.MarkItemAsLost(lostTestLoan);
        }
        catch (Exception ex)
        {
            WriteRejectedAction("Marking the same lost loan as lost again", ex);
        }

        Console.WriteLine("\n-- Protected / controlled state demonstrations completed --");
    }

    private static void WriteRejectedAction(string action, Exception exception)
    {
        Console.WriteLine(action + " was rejected: " + exception.Message);
    }
}
