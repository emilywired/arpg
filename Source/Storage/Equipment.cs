using System;

public enum EquipmentSlot
{
    MainHand,
    OffHand,
    Chest,
    Head,
    Gloves,
    Boots,
    Belt,
    Amulet,
    LeftRing,
    RightRing,
}

public class Equipment(Player _player)
{
    public EquippableItem? MainHand { get; private set; }
    public EquippableItem? OffHand { get; private set; }
    public EquippableItem? Chest { get; private set; }
    public EquippableItem? Head { get; private set; }
    public EquippableItem? Gloves { get; private set; }
    public EquippableItem? Boots { get; private set; }
    public EquippableItem? Belt { get; private set; }
    public EquippableItem? Amulet { get; private set; }
    public EquippableItem? LeftRing { get; private set; }
    public EquippableItem? RightRing { get; private set; }

    private Player player = _player;

    public void Equip(EquippableItem item)
    {
        switch (item.Slot)
        {
            case EquippableSlot.MainHand:
                MainHand = item;
                break;
            case EquippableSlot.OffHand:
                OffHand = item;
                break;
            case EquippableSlot.Chest:
                Chest = item;
                break;
            case EquippableSlot.Head:
                Head = item;
                break;
            case EquippableSlot.Gloves:
                Gloves = item;
                break;
            case EquippableSlot.Boots:
                Boots = item;
                break;
            case EquippableSlot.Belt:
                Belt = item;
                break;
            case EquippableSlot.Amulet:
                Amulet = item;
                break;
            case EquippableSlot.Ring:
                if (LeftRing == null)
                {
                    LeftRing = item;
                    break;
                }

                if (RightRing == null)
                {
                    RightRing = item;
                    break;
                }

                bool unequipped = Unequip(LeftRing);
                if (!unequipped)
                {
                    throw new Exception(
                        "Failed to unequip equipped item during equipping another."
                    );
                }

                LeftRing = item;

                break;
            default:
                throw new NotImplementedException("Unhandled EquippableSlot");
        }

        _ = player.Inventory.RemoveItem(item);
        item.Equip(player);
    }

    public bool Unequip(EquippableItem item)
    {
        if (!ItemIsEquipped(item))
            throw new Exception("Cannot unequip unequipped item.");

        bool movedToInventory = player.Inventory.AddItem(item);

        if (!movedToInventory)
            return false;

        switch (item.Slot)
        {
            case EquippableSlot.MainHand:
                MainHand = null;
                break;
            case EquippableSlot.OffHand:
                OffHand = null;
                break;
            case EquippableSlot.Chest:
                Chest = null;
                break;
            case EquippableSlot.Head:
                Head = null;
                break;
            case EquippableSlot.Gloves:
                Gloves = null;
                break;
            case EquippableSlot.Boots:
                Boots = null;
                break;
            case EquippableSlot.Belt:
                Belt = null;
                break;
            case EquippableSlot.Amulet:
                Amulet = null;
                break;
            case EquippableSlot.Ring:
                if (LeftRing == item)
                {
                    LeftRing = null;
                }
                else if (RightRing == item)
                {
                    RightRing = null;
                }
                else
                {
                    throw new Exception("Ring fucky wucky.");
                }

                break;
            default:
                throw new NotImplementedException("Unhandled EquippableSlot");
        }

        item.Unequip(player);
        return true;
    }

    private bool ItemIsEquipped(EquippableItem item)
    {
        return item.Slot switch
        {
            EquippableSlot.MainHand => MainHand == item,
            EquippableSlot.OffHand => OffHand == item,
            EquippableSlot.Chest => Chest == item,
            EquippableSlot.Head => Head == item,
            EquippableSlot.Gloves => Gloves == item,
            EquippableSlot.Boots => Boots == item,
            EquippableSlot.Belt => Belt == item,
            EquippableSlot.Amulet => Amulet == item,
            EquippableSlot.Ring => LeftRing == item || RightRing == item,
            _ => throw new NotImplementedException("Unhandled EquippableSlot"),
        };
    }
}
