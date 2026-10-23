using System;

namespace SrpLab;

public class GradeBook
{
    public TranscriptFormatter Transcript {get; }
    private readonly GradeWithLetter _letter;
    private readonly HonorRollEligibility _honor;
    
    public GradeBook()
    {
        _letter = new GradeWithLetter(this);
        _honor = new HonorRollEligibility(this, _letter);
        Transcript = new TranscriptFormatter(this, _letter, _honor);
    }

    
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);
    
    public IReadOnlyDictionary<string, List<decimal>> GetScores() => _scores;
    
    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }
        list.Add(score);
    }
    
    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list) || list.Count == 0) return 0m;
        return Math.Round(list.Average(), 2);
    }

}
