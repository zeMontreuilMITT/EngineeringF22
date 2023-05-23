Beverage DarkRoast = new DarkRoast();
DarkRoast.Cost();

DarkRoast = new Mocha(DarkRoast);
Console.WriteLine(DarkRoast.Cost().ToString());
Console.WriteLine(DarkRoast.Description());


DarkRoast = new EspressoShot(DarkRoast);
Console.WriteLine(DarkRoast.Cost().ToString());
Console.WriteLine(DarkRoast.Description());

DarkRoast = new MembershipPerk(DarkRoast);
Console.WriteLine(DarkRoast.Cost().ToString());
Console.WriteLine(DarkRoast.Description());
public abstract class Beverage
{
    protected string _description = "Unknown Beverage";
    public virtual string Description()
    {
        return _description;
    }
    public abstract decimal Cost();
}

#region baseConcrete
// Base Concrete Classes
public class DarkRoast : Beverage
{
    // no beverage property here because they cannot decorate other classes
    public DarkRoast()
    {
        _description = "Dark Roast Coffee";
    }

    // return their own values rather than delegating to the decorated object's method
    public override decimal Cost()
    {
        return 1.99M;
    }
}

public class Espresso: Beverage
{

    public Espresso()
    {
        _description = "Espresso Coffee";
    }

    public override decimal Cost()
    {
        return 2.79M;
    }

}
#endregion

#region Decorators
// ABSTRACT DECORATOR
public abstract class CondimentDecorator : Beverage
{
    // reference to the decorated object
    protected Beverage _beverage;
    public abstract override string Description();
}

// CONCRETE DECORATORS
// Concrete decorator inherits from abstract decorator
public class EspressoShot : CondimentDecorator
{
    public EspressoShot(Beverage decoratedBeverage)
    {
        _beverage = decoratedBeverage;
    }

    public override string Description()
    {
        return _beverage.Description() + ", Espresso Shot";
    }

    public override decimal Cost()
    {
        return 0.20M + _beverage.Cost();
    }
}

public class Mocha : CondimentDecorator
{
    public Mocha(Beverage decoratedBeverage)
    {
        _beverage = decoratedBeverage;
    }

    public override string Description()
    {
        return _beverage.Description() + ", Mocha flakes";
    }

    public override decimal Cost()
    {
        return 0.50M + _beverage.Cost();
    }
}

// TODO: Add method of preventing membership being added to anything but the final decorator
public class MembershipPerk : CondimentDecorator { 

    public MembershipPerk(Beverage decoratedBeverage)
    {
        _beverage = decoratedBeverage;
    }

    public override decimal Cost()
    {
        AddPointsToUser(_beverage.Cost());
        return _beverage.Cost() / 2;
    }

    public override string Description()
    {
        return _beverage.Description() + ", half off!";
    }

    private void AddPointsToUser(decimal cost)
    {
        // make a call to user DB and increment points

        Console.WriteLine($"You received {cost / 10} points for your account.");
    }
}
#endregion