using VintedShipmentCalculator.Models;

namespace VintedShipmentCalculator.Rules;

public interface IDiscountRule
{
    void Apply(List<Shipment> shipments);
}
