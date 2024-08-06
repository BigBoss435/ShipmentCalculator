using System;

namespace VintedShipmentCalculator.Models;

public class Shipment
{
    public DateTime Date { get; set; }
    public char Size { get; set; }
    public string Carrier { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set;  }

    public Shipment(string line)
    {
        var parts = line.Split(' ');
        if (parts.Length != 3 ||
            !DateTime.TryParse(parts[0], out DateTime date) ||
            !char.TryParse(parts[1], out char size) ||
            string.IsNullOrEmpty(parts[2]))
        {
            throw new FormatException("Invalid line format");
        }

        Date = date;
        Size = size;
        Carrier = parts[2];
    }

    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd} {Size} {Carrier} {Price:0.00} {(Discount > 0 ? Discount.ToString("0.00") : "-")}";
    }
}
