using System.Collections.Generic;

public enum EquippableSlot
{
    MainHand,
    OffHand,
    Chest,
    Head,
    Gloves,
    Boots,
    Belt,
    Amulet,
    Ring,
}

public interface IEquippable
{
    bool IsEquipped { get; }
    EquippableSlot Slot { get; }
    void Equip(Player player);
    void Unequip(Player player);
}
