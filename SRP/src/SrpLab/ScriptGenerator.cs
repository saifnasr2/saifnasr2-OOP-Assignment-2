using System;

namespace SrpLab;

public class ScriptGenerator
{
    private readonly WarehouseManagement _wareHouse;
    private readonly WarehouseOrganizer _organizer;

    public ScriptGenerator(WarehouseManagement warehouse, WarehouseOrganizer organizer)
    {
        _wareHouse = warehouse;
        _organizer = organizer;
    }

    public string PickerScript()
    {
        var steps = _organizer.WalkingOrder()
            .Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");
        var shortfalls = _wareHouse.Allocate().Where(a =>
        {
            var need = _wareHouse.GetLines().First(l => l.Sku == a.Sku).QtyNeeded;   
            return a.Allocated < need;
        });

        var warn = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls.Select(s => s.Sku))
            : "SHORTAGES: none";
        return string.Join('\n', steps) + "\n" + warn;
    }
}
