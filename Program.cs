Game.Confrontation();
public static class Game
{
    private static PlayerCharacter _character;
    public static Opponent Opponent;

    static Game()
    {
        _character = new PlayerCharacter();
        Opponent = new Jerk();

        _character.SetEquipment(new EquipmentItem());
        _character.DecorateEquipment(new DiplomacyDecorator());
        _character.DecorateEquipment(new BlackmailDecorator());


    }

    public static void Confrontation()
    {
        _character.GetEquipmentAttributes();

        if (Opponent.Defeated)
        {
            Console.WriteLine("Player wins");
        } else
        {
            Console.WriteLine("Opponent wins");
        }
    }
}

#region opponents
public abstract class Opponent
{
    public bool Defeated { get; set; }
}

public class Jerk: Opponent, IDiplomacyVulnerable
{

}

public class Misfortunate: Opponent, IBlackmailVulnerable
{

}

public class Goblin: Opponent, IBlackmailVulnerable, IDiplomacyVulnerable
{

}

public interface IDiplomacyVulnerable
{

}

public interface IBlackmailVulnerable
{

}
#endregion

#region base and concrete
public abstract class EquipmentEntity
{
    public abstract void GetEquipmentAttributes();
}

public class EquipmentItem : EquipmentEntity
{
    public override void GetEquipmentAttributes()
    {
        Console.WriteLine("This is a piece of equipment.");
    }
}

public class PlayerCharacter
{
    private EquipmentEntity _equippedItem;
    public void SetEquipment(EquipmentEntity item)
    {
        if(item is EquipmentItem)
        {
            _equippedItem = item;
        }
    }

    public void GetEquipmentAttributes()
    {
        _equippedItem.GetEquipmentAttributes();
    }

    public PlayerCharacter(EquipmentEntity item)
    {
        _equippedItem = item;
    }

    public PlayerCharacter()
    {
    }

    public void DecorateEquipment(EquipmentDecorator decorator)
    {
        // this method allows us to keep EquippedItem as a reference to the most recently instantiated Decorator object
        decorator.DecoratedReference = _equippedItem;
        _equippedItem = decorator;
    }
}
#endregion

#region decorator classes
public abstract class EquipmentDecorator : EquipmentEntity
{
    public EquipmentEntity DecoratedReference { get; set; }
    public abstract override void GetEquipmentAttributes();

    public EquipmentDecorator()
    {

    }
    public EquipmentDecorator(EquipmentEntity decoratedReference)
    {
        DecoratedReference = decoratedReference;
    }
}
public class DiplomacyDecorator : EquipmentDecorator
{
    public DiplomacyDecorator(EquipmentEntity decoratedReference) : base(decoratedReference)
    {
    }

    public DiplomacyDecorator()
    {

    }

    public override void GetEquipmentAttributes()
    {
        DecoratedReference.GetEquipmentAttributes();
        Console.WriteLine("The equipment is imbued with the power of diplomacy.");

        if(Game.Opponent is IDiplomacyVulnerable)
        {
            Console.WriteLine("The enemy is vulnerable to diplomacy!");
            Game.Opponent.Defeated = true;
        } else
        {
            Console.WriteLine("The opponent shuns all attempt at diplomacy.");
        }
    }

}
public class BlackmailDecorator : EquipmentDecorator
{
    public BlackmailDecorator(EquipmentEntity decoratedReference) : base(decoratedReference)
    {
    }

    public BlackmailDecorator()
    {
       
    }

    public override void GetEquipmentAttributes()
    {
        DecoratedReference.GetEquipmentAttributes();
        Console.WriteLine("The equipment is imbued with the power of Blackmail.");

        if (Game.Opponent is IBlackmailVulnerable)
        {
            Console.WriteLine("The opponent is vulnerable to blackmail!");
            Game.Opponent.Defeated = true;
        }
        else
        {
            Console.WriteLine("The opponent is indifferent to blackmail.");
        }
    }

}
#endregion