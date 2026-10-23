using System;

namespace SrpLab;

public class ItemManagement
{
    private readonly AllergenDetector _detector;
    private readonly PreparationTimeEstimator _timeEstimator;
    public ThermalTicketFormatter ThermalTicket {get; }
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

    public ItemManagement()
    {
        _detector = new AllergenDetector(this);
        _timeEstimator = new PreparationTimeEstimator(this, _detector);
        ThermalTicket = new ThermalTicketFormatter(this, _detector, _timeEstimator);
    }


    public IReadOnlyList<(string Item, List<string> Ingredients, int PrepMinutes)> GetItems() => _items;
    
    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((item, ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    }

}
