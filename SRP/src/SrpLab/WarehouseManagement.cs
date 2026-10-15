using System;

namespace SrpLab;

public class WarehouseManagement
{
    private readonly WarehouseOrganizer _organizer;
    public ScriptGenerator Script {get; }
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    public WarehouseManagement()
    {
        _organizer = new WarehouseOrganizer(this);
        Script = new ScriptGenerator(this, _organizer);
    }

    public IReadOnlyList<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> GetLines() => _lines;

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        var result = new List<(string, int)>();
        foreach(var line in _lines)
        {
            var alloc = Math.Min(line.QtyNeeded, line.QtyOnHand);
            result.Add((line.Sku, alloc));
        }
        return result;
    }
}
