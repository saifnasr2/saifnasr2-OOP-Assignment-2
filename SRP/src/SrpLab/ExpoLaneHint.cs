using System;

namespace SrpLab;

public class ExpoLaneHint
{
    private readonly AllergenDetector _detector;
    private readonly PreparationTimeEstimator _timer;

    public ExpoLaneHint(AllergenDetector detector, PreparationTimeEstimator timer)
    {
        _detector = detector;
        _timer = timer;
    }

    public string DetermineExpoLane()
    {
        return _detector.DetectAllergens().Count > 0 ? "LANE-ALLERGY" : _timer.EstimatedReadyMinutes(2) > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}
