using System;

namespace SrpLab;

public class PreparationTimeEstimator
{
    private readonly ItemManagement _item;
    private readonly AllergenDetector _detector;

    public PreparationTimeEstimator(ItemManagement item, AllergenDetector detector)
    {
        _item = item;
        _detector = detector;
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        if (openStations <= 0) openStations = 1;
        var sequential = _item.GetItems().Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (_detector.DetectAllergens().Count > 0) parallel += 3;
        var longest = _item.GetItems().Count == 0 ? 0 : _item.GetItems().Max(i => i.PrepMinutes);
        return Math.Max(longest, parallel);
    }
}
