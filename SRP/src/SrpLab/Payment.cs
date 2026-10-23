using System;
using System.Runtime.CompilerServices;

namespace SrpLab;

public class Payment
{
    private readonly CheckoutBasket _basket;
    private readonly CalculateAmount _calculate;

    public Payment(CheckoutBasket basket)
    {
        _basket = basket;
        _calculate = new CalculateAmount(basket);
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        var payload = $"{_calculate.GrandTotal():0:00}|{cardLast4}|{_basket.GetList().Count}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }

}
