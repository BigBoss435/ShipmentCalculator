using Xunit;
using VintedShipmentCalculator.Models;
using VintedShipmentCalculator.Rules;

namespace VintedShipmentCalculator.Tests;

public class ShipmentProcessorTests
{
    [Fact]
    public void TestSmallPackageLowestPriceRule()
    {
        var shipments = new List<Shipment>
        {
            new Shipment("2015-02-01 S MR") { Price = 2.00m }
        };

        var rule = new SmallPackageLowestPriceRule(1.50m);
        rule.Apply(shipments);
        
        Assert.Equal(1.50m, shipments[0].Price);
        Assert.Equal(0.50m, shipments[0].Discount);
    }

    [Fact]
    public void TestThirdLShipmentFreeRule()
    {
        var shipments = new List<Shipment>()
        {
            new Shipment("2015-02-01 L LP") { Price = 6.90m },
            new Shipment("2015-02-02 L LP") { Price = 6.90m },
            new Shipment("2015-02-03 L LP") { Price = 6.90m }
        };

        var rule = new ThirdLShipmentFreeRule();
        rule.Apply(shipments);
        
        Assert.Equal(6.90m, shipments[2].Discount);
        Assert.Equal(0.00m, shipments[2].Price);
    }

    [Fact]
    public void TestMaxAccumulatedDiscountRule()
    {
        var shipments = new List<Shipment>
        {
            new Shipment("2015-02-01 S MR") { Price = 1.50m, Discount = 0.50m },
            new Shipment("2015-02-02 S MR") { Price = 1.50m, Discount = 0.50m },
            new Shipment("2015-02-03 L LP") { Price = 0.00m, Discount = 6.90m },
            new Shipment("2015-02-05 S MR") { Price = 1.50m, Discount = 0.50m },
            new Shipment("2015-02-05 S MR") { Price = 1.50m, Discount = 0.50m },
            new Shipment("2015-02-06 S MR") { Price = 1.50m, Discount = 0.50m }
        };

        var rule = new MaxAccumulatedDiscountRule(10.00m);
        rule.Apply(shipments);

        Assert.Equal(1.50m, shipments[5].Price);
        Assert.Equal(0.00m, shipments[5].Discount);
    }
}