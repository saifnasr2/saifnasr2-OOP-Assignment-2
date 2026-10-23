namespace SrpLab;

public class CalculateAmount
{
    private CheckoutBasket _basket1;
    

    public CalculateAmount(CheckoutBasket basket)
    {
        _basket1 = basket;
        
    }

    public decimal SubTotal()
    {
       return _basket1.GetList().Sum(l => l.Price * l.Qty);
    }

    public decimal DiscountAmount()
    {
        var _couponRaw = _basket1.GetCouponRaw();
        if (string.IsNullOrWhiteSpace(_couponRaw)) return 0m;
        var t = _couponRaw.Trim().ToUpperInvariant();

        if (t.StartsWith("SAVE") && int.TryParse(t[4..], out var pct) && pct is > 0 and <= 50)
            return Math.Round(SubTotal() * pct / 100m, 2);
        if (t.Contains("FREESHIP")) return 0m;
        if (t == "WELCOME10") return Math.Min(10m, SubTotal());

        return 0m;
    }

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();
        if (_basket1.GetGiftWrap()) total += 4.99m;

        return Math.Max(0m, total);
    }
}
