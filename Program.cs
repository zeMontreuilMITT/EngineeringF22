// base abstract class that is inherited by the decorator and base concrete classes

// Polymorphism: an instance of a class can be declared as a parent and then later instantiated as any instance of a child of that class at runtime

// "Family Tree" of equipmentEntity:
// Can be instantiated as an EquipmentItem OR a child of EquipmentDecorator

EquipmentEntity DecoratedBook;

DecoratedBook = new EquipmentItem(); // valid because EquipmentItem is a child of EquipmentEntity

PlayerCharacter player = new PlayerCharacter(DecoratedBook);
player.DecorateEquipment(new DiplomacyDecorator());
player.DecorateEquipment(new BlackmailDecorator());

player.EquippedItem.GetEquipmentAttributes();


#region base and concrete
public abstract class EquipmentEntity
{
    public abstract void GetEquipmentAttributes();
}

public class EquipmentItem : EquipmentEntity
{
    public override void GetEquipmentAttributes()
    {
        Console.WriteLine("Base item attributes");
    }
}

public class PlayerCharacter
{
    private EquipmentEntity _equippedItem;
    public EquipmentEntity EquippedItem
    {
        get { return _equippedItem; }
        set
        {
            if (value is EquipmentItem)
            {
                _equippedItem = value;
            }
        }
    }
    public PlayerCharacter(EquipmentEntity item)
    {
        EquippedItem = item;
    }

    public void DecorateEquipment(EquipmentDecorator decorator)
    {
        // this method allows us to keep EquippedItem as a reference to the most recently instantiated Decorator object
        decorator.DecoratedReference = EquippedItem;
        EquippedItem = decorator;
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
        Console.WriteLine("Player wins against Diplomacy vulnerability");
        DecoratedReference.GetEquipmentAttributes();
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
        Console.WriteLine("Player wins against Blackmail vulnerability");
        DecoratedReference.GetEquipmentAttributes();
    }

}
#endregion