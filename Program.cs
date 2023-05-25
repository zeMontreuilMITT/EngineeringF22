#region pizza interfaces and implementations

PizzaStore american = new AmericanPizzaStore();
string canadianType = "Canadian";
string cheeseType = "Cheese";
PizzaStore canadian = new CanadianPizzaStore();



    american.OrderPizza(cheeseType);
    canadian.OrderPizza(cheeseType);


    american.OrderPizza(canadianType);
    canadian.OrderPizza(canadianType);

public interface Pizza
{
    void Prepare();
    void Bake();
    void Cut();
    void Box();
}
public class CheesePizza : Pizza
{
    public void Bake()
    {
        Console.WriteLine("Bake at 400 degrees for 20 minutes");
    }

    public void Box()
    {
        Console.WriteLine("Box with Cheese Pizza label");
    }

    public void Cut()
    {
        Console.WriteLine("Cut into squares.");
    }

    public void Prepare()
    {
        Console.WriteLine("Add Cheese and pizza sauce.");
    }
}
public class DoubleCheesePizza: Pizza
{
    public void Bake()
    {
        Console.WriteLine("Bake at 425 degrees for 20 minutes");
    }

    public void Box()
    {
        Console.WriteLine("Box with Double Cheese Pizza label");
    }

    public void Cut()
    {
        Console.WriteLine("Cut into squares.");
    }

    public void Prepare()
    {
        Console.WriteLine("Add double cheese and pizza sauce.");
    }
}
public class PepperoniPizza : Pizza
{
    public void Bake()
    {
        Console.WriteLine("Bake at 425 degrees for 22 minutes");
    }

    public void Box()
    {
        Console.WriteLine("Box with Pepperoni Pizza label");
    }

    public void Cut()
    {
        Console.WriteLine("Cut into wedges.");
    }

    public void Prepare()
    {
        Console.WriteLine("Add cheese, pepperoni, and pizza sauce.");
    }
}
public class CanadianPizza : Pizza
{
    public void Bake()
    {
        Console.WriteLine("Bake at 425 degrees for 23 minutes");
    }

    public void Box()
    {
        Console.WriteLine("Box with Canadian Pizza label");
    }

    public void Cut()
    {
        Console.WriteLine("Cut into wedges.");
    }

    public void Prepare()
    {
        Console.WriteLine("Add cheese, pepperoni, bacon, mushrooms, and pizza sauce.");
    }
}
#endregion

#region store classes
public abstract class PizzaStore
{
    public void SealPizzaBox()
    {
        Console.WriteLine("Sealing the pizza box.");
    }
    public Pizza OrderPizza(string type)
    {
        Pizza pizza;
        try {

            // decide what type of pizza it is

            pizza = ChoosePizza(type);
            // we expect that this code is going to change regularly

            // no matter what subclass of Pizza is chosen, this code will never change
            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();
            SealPizzaBox();

        } catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        return pizza;
    }

    public abstract Pizza ChoosePizza(string type);
}

public class CanadianPizzaStore : PizzaStore
{
    public override Pizza ChoosePizza(string type)
    {
        Pizza newPizza;
        if(type == "Pepperoni")
        {
            newPizza = new PepperoniPizza();
        } else if (type =="Cheese")
        {
            newPizza = new CheesePizza();
        } else if (type == "Canadian")
        {
            newPizza = new CanadianPizza();
        } else
        {
            throw new ArgumentException($"Invalid selection type of '{type}'");
        }

        return newPizza;
    }
}
public class AmericanPizzaStore : PizzaStore
{
    public override Pizza ChoosePizza(string type)
    {
        Pizza newPizza;
        if (type == "Pepperoni")
        {
            newPizza = new PepperoniPizza();
        }
        else if (type == "Cheese")
        {
            newPizza = new DoubleCheesePizza();
        }
        else
        {
            throw new ArgumentException($"Invalid selection type of '{type}'");
        }

        return newPizza;
    }
}
#endregion