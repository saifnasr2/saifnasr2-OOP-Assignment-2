using System;

namespace SrpLab;

public class HonorRollEligibility
{
    private readonly GradeBook _gradeBook;
    private readonly GradeWithLetter _letter;

    public HonorRollEligibility(GradeBook gradeBook, GradeWithLetter letter)
    {
        _gradeBook = gradeBook;
        _letter = letter;
    }
    public bool MeetsHonorRoll(string studentId)
    {
        return _gradeBook.Average(studentId) >= 85 && _letter.Letter(studentId) is "A" or "B";
    }

}
