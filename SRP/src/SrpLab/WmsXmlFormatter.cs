using System;

namespace SrpLab;

public class WmsXmlFormatter
{
    private readonly WarehouseManagement _warehouse;
    public WmsXmlFormatter(WarehouseManagement warehouse)
    {
        _warehouse = warehouse;
    }

    public string WmsXmlBatch(string batchId)
    {
        var parts = _warehouse.Allocate().Select(a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");
        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}
