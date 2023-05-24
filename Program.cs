
Car civic = new Vehicle(30000);
civic = new HybridEngine(civic);

Console.WriteLine(civic.GetPrice());

// base abstract class
public abstract class Car
{
    protected decimal _price;
    public abstract decimal GetPrice();
}


// base concrete classes
class Vehicle: Car
{
    public Vehicle(decimal price)
    {
        _price = price;
    }

    public int Year { get; set; }
    public string Model { get; set; }
    public string Colour { get; set; }
    public string BodyType { get; set; }
    public override decimal GetPrice()
    {
        return _price;
    }
}

public abstract class CarUpgrade: Car
{
    protected Car _decoratedReference;

    public CarUpgrade(Car decoratedReference)
    {
        _decoratedReference = decoratedReference;
    }

    public abstract override decimal GetPrice();
}

public class LeatherSeats : CarUpgrade
{
    public LeatherSeats(Car decoratedReference) : base(decoratedReference)
    {
    }

    public override decimal GetPrice()
    {
        return _decoratedReference.GetPrice() + 500;
    }
}
public class HybridEngine : CarUpgrade
{
    public HybridEngine(Car decoratedReference) : base(decoratedReference)
    {
    }

    public override decimal GetPrice()
    {
        double discountPercent = _getEnviroDiscountPercent();

        decimal basePrice = _decoratedReference.GetPrice();
        decimal discount = (basePrice * (decimal)discountPercent);

        return basePrice - discount + 2000;
    }

    private double _getEnviroDiscountPercent()
    {
        // fetches number from government API
        return .20;
    }
}