using System;

namespace SrpLab;

public class AllergenDetector
{
    private readonly ItemManagement _item;

    public AllergenDetector(ItemManagement item)
    {
        _item = item;
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        var hits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach(var(_, ingredients, _) in _item.GetItems())
        {
            foreach(var ing in ingredients)
            {
                if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter")) hits.Add("dairy");
                if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread")) hits.Add("gluten");
                if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew")) hits.Add("nuts");
                if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab")) hits.Add("shellfish");
            }
        }
        
        return hits.OrderBy(x => x).ToList();
    }
}
