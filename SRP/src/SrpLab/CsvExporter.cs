using System;

namespace SrpLab;

public class CsvExporter
{
    private readonly GradeBook _gradeBook;
    private readonly GradeWithLetter _letter;
    private readonly HonorRollEligibility _honorRoll;

    public CsvExporter(GradeBook gradeBook, GradeWithLetter letter, HonorRollEligibility honorRoll)
    {
        _gradeBook = gradeBook;
        _letter = letter;
        _honorRoll = honorRoll;
    }

    public string ExportCsv()
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var id in _gradeBook.GetScores().Keys.OrderBy(x => x))
            rows.Add($"{id},{_gradeBook.Average(id)},{_letter.Letter(id)},{(_honorRoll.MeetsHonorRoll(id) ? 1 : 0)}");
        return string.Join('\n', rows);
    }   
}
