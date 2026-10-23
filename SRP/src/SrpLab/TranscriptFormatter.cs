using System;

namespace SrpLab;

public class TranscriptFormatter
{
    private readonly GradeBook _gradeBook;
    private readonly GradeWithLetter _letter;
    private readonly HonorRollEligibility _honorRoll;

    public TranscriptFormatter(GradeBook gradeBook, GradeWithLetter letter, HonorRollEligibility honorRoll)
    {
        _gradeBook = gradeBook;
        _letter = letter;
        _honorRoll = honorRoll;
    }

 
    public string TranscriptPlain(string studentId, string fullName)
    {
        // Registrar document format ≠ grading policy.
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {_gradeBook.Average(studentId)}\nLetter: {_letter.Letter(studentId)}\nHonor: {_honorRoll.MeetsHonorRoll(studentId)}\n";
    }
   

}
