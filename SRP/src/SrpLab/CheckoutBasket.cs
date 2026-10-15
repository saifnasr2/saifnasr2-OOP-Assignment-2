using System;

namespace SrpLab;

public class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;
   

    public IReadOnlyList<(string Sku, decimal Price, int Qty)> GetList() => _lines;
    public string? GetCouponRaw() => _couponRaw;
    public bool GetGiftWrap() => _giftWrap;
    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }


    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;

    public string GitMessageCard(string fromName)
    {
        var grandTotal = new CalculateAmount(this).GrandTotal();
        var items = string.Join(", ", _lines.Select(l => l.Sku));
        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {grandTotal:C}\n";
    }
}

