using VintedShipmentCalculator.Models;
using VintedShipmentCalculator.Rules;

namespace VintedShipmentCalculator;

public class ShipmentProcessor
{
    private readonly List<IDiscountRule> _rules;

    public ShipmentProcessor(List<IDiscountRule> rules)
    {
        _rules = rules;
    }

    public List<Shipment> Process(string inputFile)
    {
        var shipments = new List<Shipment>();

        foreach (var line in File.ReadLines(inputFile))
        {
            try
            {
                var shipment = new Shipment(line);
                shipment.Price = GetPrice(shipment.Size, shipment.Carrier);
                shipments.Add(shipment);
            }
            catch (FormatException)
            {
                Console.WriteLine($"{line} Ignored");
            }
        }

        foreach (var rule in _rules)
        {
            rule.Apply(shipments);
        }

        return shipments;
    }

    private decimal GetPrice(char size, string carrier)
    {
        return (carrier, size) switch
        {
            ("LP", 'S') => 1.50m,
            ("LP", 'M') => 4.90m,
            ("LP", 'L') => 6.90m,
            ("MR", 'S') => 2.00m,
            ("MR", 'M') => 3.00m,
            ("MR", 'L') => 4.00m,
            _ => throw new FormatException("Unrecognized carrier or size")
        };
    }
}