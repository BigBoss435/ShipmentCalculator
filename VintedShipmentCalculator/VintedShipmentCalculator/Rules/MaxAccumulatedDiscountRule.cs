using VintedShipmentCalculator.Models;

namespace VintedShipmentCalculator.Rules;

public class MaxAccumulatedDiscountRule : IDiscountRule
{
    private readonly decimal _maxMonthlyDiscount;

    public MaxAccumulatedDiscountRule(decimal maxMonthlyDiscount)
    {
        _maxMonthlyDiscount = maxMonthlyDiscount;
    }

    public void Apply(List<Shipment> shipments)
    {
        var monthlyDiscounts = shipments
            .GroupBy(s => new { s.Date.Year, s.Date.Month })
            .ToDictionary(g => g.Key, g => g.Sum(s => s.Discount));

        foreach (var shipment in shipments)
        {
            var key = new { shipment.Date.Year, shipment.Date.Month };
            if (monthlyDiscounts[key] > _maxMonthlyDiscount)
            {
                var excess = monthlyDiscounts[key] - _maxMonthlyDiscount;
                if (shipment.Discount >= excess)
                {
                    shipment.Discount -= excess;
                    shipment.Price += excess;
                    monthlyDiscounts[key] -= excess;
                }
                else
                {
                    shipment.Price += shipment.Discount;
                    monthlyDiscounts[key] -= shipment.Discount;
                    shipment.Discount = 0;
                }
            }
        }
    }
}
