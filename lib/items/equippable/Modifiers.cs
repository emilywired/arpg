using System.Collections.Generic;

public interface Modifier { }

public class Modifiers
{
    public class Global
    {
        public Dictionary<EquippableSlot, List<Modifier>> Prefixes = new()
        {
            { EquippableSlot.MainHand, [] },
            { EquippableSlot.OffHand, [] },
            { EquippableSlot.Chest, [] },
            { EquippableSlot.Head, [] },
            { EquippableSlot.Gloves, [] },
            { EquippableSlot.Boots, [] },
            { EquippableSlot.Belt, [] },
            { EquippableSlot.Amulet, [] },
            { EquippableSlot.Ring, [] },
        };

        public Dictionary<EquippableSlot, List<Modifier>> Suffixes = new()
        {
            { EquippableSlot.MainHand, [] },
            { EquippableSlot.OffHand, [] },
            { EquippableSlot.Chest, [] },
            { EquippableSlot.Head, [] },
            { EquippableSlot.Gloves, [] },
            { EquippableSlot.Boots, [] },
            { EquippableSlot.Belt, [] },
            { EquippableSlot.Amulet, [] },
            { EquippableSlot.Ring, [] },
        };
    }
}
