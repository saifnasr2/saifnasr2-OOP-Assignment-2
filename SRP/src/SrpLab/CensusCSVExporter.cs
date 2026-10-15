namespace SrpLab;

public class CensusCSVExporter
{
    private readonly IReadOnlyDictionary<int, string> _bedPatient;
    private readonly IReadOnlyDictionary<int, int> _vitalsScore;

    public CensusCSVExporter(WardBoard wardBoard)
    {
        
        _bedPatient = wardBoard.GetBedPatient();
        _vitalsScore = wardBoard.GetVitalsScore();
        
    }
    

    public string GenerateCSVFile()
    {
        
        var lines = new List<string> {"bed,patient,acuity"};
        foreach (var bed in _bedPatient.Keys.OrderBy(x => x))
        {
            lines.Add($"{bed},{_bedPatient[bed]},{_vitalsScore[bed]}");
        }

        return string.Join('\n' , lines);
    }   
}
