namespace SrpLab;

public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly List<string> _pagerlog = new();
    private readonly Dictionary<int, int> _vitalsScore = new();
    
    public IReadOnlyDictionary<int, string> GetBedPatient()
    {
        return _bedPatient;
    }
    public IReadOnlyDictionary<int, int> GetVitalsScore()
    {
        
        return _vitalsScore;
    }

    public void AssignBed(int bed , string patientId , int heartRate , int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");
    
        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = AcuityScorer.ScoreAcuity(heartRate , spo2);

        if (_vitalsScore[bed] >= 8)
            _pagerlog.Add($"Code-Yellow bed={bed} at {DateTime.UtcNow:HH:mm}");
           
    }

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";
        
        var acuity = _vitalsScore[bed];
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";

        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity{acuity} · {tone}";
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        var copy = _pagerlog.ToList();

        _pagerlog.Clear();

        return copy;
    }

}
