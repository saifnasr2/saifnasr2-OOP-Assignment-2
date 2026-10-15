using System;

namespace SrpLab;

public class GradeWithLetter
{
    private readonly GradeBook _gradeBook;

    public GradeWithLetter(GradeBook gradeBook)
    {
        _gradeBook = gradeBook;
    }

    public string Letter(string studentId)
    {
        
        var avg = _gradeBook.Average(studentId);
        if (avg >= 90) return "A";
        if (avg >= 80) return "B";
        if (avg >= 70) return "C";
        if (avg >= 60) return "D";
        return "F";
    }
}
