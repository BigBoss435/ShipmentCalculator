using VintedShipmentCalculator.Models;

namespace VintedShipmentCalculator.Rules;

public class ThirdLShipmentFreeRule: IDiscountRule
{
    public void Apply(List<Shipment> shipments)
    {
        var monthlyLShipments = shipments
            .Where(s => s.Size == 'L' && s.Carrier == "LP")
            .GroupBy(s => new { s.Date.Year, s.Date.Month });

        foreach (var group in monthlyLShipments)
        {
            var thirdShipment = group.Skip(2).FirstOrDefault();
            if (thirdShipment != null)
            {
                thirdShipment.Discount = thirdShipment.Price;
                thirdShipment.Price = 0;
            }
        }
    }
}
