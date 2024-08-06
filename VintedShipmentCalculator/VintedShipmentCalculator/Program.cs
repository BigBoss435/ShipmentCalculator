using System;
using System.Collections.Generic;
using VintedShipmentCalculator;
using VintedShipmentCalculator.Rules;

public class Program
{
    public static void Main(string[] args)
    {
        var rules = new List<IDiscountRule>
        {
            new SmallPackageLowestPriceRule(1.50m),
            new ThirdLShipmentFreeRule(),
            new MaxAccumulatedDiscountRule(10.00m)
        };

        var processor = new ShipmentProcessor(rules);
        var shipments = processor.Process("input.txt");

        foreach (var shipment in shipments)
        {
            Console.WriteLine(shipment);
        }
    }
}
