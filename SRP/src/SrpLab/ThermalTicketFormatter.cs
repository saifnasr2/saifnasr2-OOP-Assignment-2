using System;

namespace SrpLab;

public class ThermalTicketFormatter
{
    private readonly ItemManagement _item;
    private readonly AllergenDetector _detector;
    private readonly PreparationTimeEstimator _estimator;

    public ThermalTicketFormatter(ItemManagement item, AllergenDetector detector, PreparationTimeEstimator estimator)
    {
        _item = item;
        _detector = detector;
        _estimator = estimator;
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var width = 32;
        var line = new string('=', width);
        var body = string.Join('\n', _item.GetItems().Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergens = _detector.DetectAllergens();
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join("," ,allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {_estimator.EstimatedReadyMinutes(2)} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
