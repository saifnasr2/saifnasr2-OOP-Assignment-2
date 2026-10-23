using System;

namespace SrpLab;

public class WarehouseOrganizer
{
    private readonly WarehouseManagement _warehouse;
    public WarehouseOrganizer(WarehouseManagement warehouse)
    {
        _warehouse = warehouse;
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _warehouse.GetLines()
                        .OrderBy(l => l.Aisle)
                        .ThenBy(l => l.Bin)
                        .Select(l => (l.Aisle, l.Bin, l.Sku, Math.Min(l.QtyNeeded, l.QtyOnHand)))
                        .Where(x => x.Item4 > 0)
                        .ToList();
    }
}
