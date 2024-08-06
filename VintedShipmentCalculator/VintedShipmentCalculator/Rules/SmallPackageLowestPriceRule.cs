using VintedShipmentCalculator.Models;

namespace VintedShipmentCalculator.Rules;

public class SmallPackageLowestPriceRule : IDiscountRule
{
    private readonly decimal _lowestSPrice;

    public SmallPackageLowestPriceRule(decimal lowestSPrice)
    {
        _lowestSPrice = lowestSPrice;
    }

    public void Apply(List<Shipment> shipments)
    {
        foreach (var shipment in shipments.Where(s => s.Size == 'S'))
        {
            if (shipment.Price > _lowestSPrice)
            {
                shipment.Discount = shipment.Price - _lowestSPrice;
                shipment.Price = _lowestSPrice;
            }
        }
    }
}
