using System;
using System.Collections.Generic;

// Weights
// Tiers

public class GlobalAffixes
{
    public Dictionary<EquippableSlot, List<Tuple<int, Func<Affix>>>> Prefixes = new()
    {
        { EquippableSlot.MainHand, [] },
        { EquippableSlot.OffHand, [] },
        { EquippableSlot.Chest, [] },
        { EquippableSlot.Head, [] },
        { EquippableSlot.Gloves, [] },
        { EquippableSlot.Boots, [] },
        { EquippableSlot.Belt, [] },
        {
            EquippableSlot.Amulet,
            [
                new(1000, () => new LifeAffix(3, 6)),
                new(1000, () => new ManaAffix(3, 6)),
                new(1000, () => new LifeOnKillAffix(1)),
                new(1000, () => new ManaOnKillAffix(1)),
            ]
        },
        {
            EquippableSlot.Ring,
            [
                new(200, () => new LifeAffix(3, 6)),
                new(200, () => new ManaAffix(3, 6)),
                new(1000, () => new LifeOnKillAffix(1)),
                new(1000, () => new ManaOnKillAffix(1)),
            ]
        },
    };

    public Dictionary<EquippableSlot, List<Tuple<int, Func<Affix>>>> Suffixes = new()
    {
        { EquippableSlot.MainHand, [] },
        { EquippableSlot.OffHand, [] },
        { EquippableSlot.Chest, [] },
        { EquippableSlot.Head, [] },
        { EquippableSlot.Gloves, [] },
        { EquippableSlot.Boots, [] },
        { EquippableSlot.Belt, [] },
        { EquippableSlot.Amulet, [] },
        {
            EquippableSlot.Ring,
            [
                new(1000, () => new StrengthAffix(1, 2)),
                new(1000, () => new AgilityAffix(1, 2)),
                new(1000, () => new IntelligenceAffix(1, 2)),
                new(1000, () => new VitalityAffix(1, 2)),
                new(1000, () => new SpiritAffix(1, 2)),
            ]
        },
    };
}
