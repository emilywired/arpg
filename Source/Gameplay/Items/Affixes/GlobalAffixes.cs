using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

public enum AffixId
{
    LesserLife,
    GreaterLife,
    Mana,
    LifeOnKill,
    ManaOnKill,
    FireResistance,
    ColdResistance,
    LightningResistance,
    Strength,
    Agility,
    Intelligence,
    Vitality,
    Spirit,
}

public record AffixTierInfo(
    int Tier, // 0 = best
    int Weight,
    int RequiredItemLevel,
    Func<Affix> CreateAffix
);

public record AffixFamily(AffixId Id, ImmutableArray<AffixTierInfo> Tiers)
{
    public readonly int TotalWeight = Tiers.Sum(t => t.Weight);
};

public static class GlobalAffixes
{
    private static AffixTierInfo MovementSpeedCorrupt = new(
        0,
        1000,
        1,
        () => new MovementSpeedAffix(7, 10)
    );

    private static AffixTierInfo LifeCorrupt = new(0, 3000, 1, () => new LifeAffix(30, 40));

    public static ImmutableDictionary<
        EquippableSlot,
        ImmutableArray<AffixTierInfo>
    > CorruptedImplicits = new Dictionary<EquippableSlot, ImmutableArray<AffixTierInfo>>()
    {
        { EquippableSlot.MainHand, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.OffHand, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.Chest, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.Head, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.Gloves, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.Boots, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.Belt, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.Amulet, [MovementSpeedCorrupt, LifeCorrupt] },
        { EquippableSlot.Ring, [MovementSpeedCorrupt, LifeCorrupt] },
    }.ToImmutableDictionary();

    public static ImmutableDictionary<EquippableSlot, ImmutableArray<AffixFamily>> Prefixes =
        new Dictionary<EquippableSlot, ImmutableArray<AffixFamily>>()
        {
            { EquippableSlot.MainHand, [GreaterLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.OffHand, [GreaterLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.Chest, [GreaterLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.Head, [GreaterLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.Gloves, [GreaterLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.Boots, [GreaterLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.Belt, [GreaterLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.Amulet, [LesserLife, Mana, LifeOnKill, ManaOnKill] },
            { EquippableSlot.Ring, [LesserLife, Mana, LifeOnKill, ManaOnKill] },
        }.ToImmutableDictionary();

    public static ImmutableDictionary<EquippableSlot, ImmutableArray<AffixFamily>> Suffixes =
        new Dictionary<EquippableSlot, ImmutableArray<AffixFamily>>()
        {
            { EquippableSlot.MainHand, [Strength, Agility, Intelligence, Vitality, Spirit] },
            { EquippableSlot.OffHand, [Strength, Agility, Intelligence, Vitality, Spirit] },
            {
                EquippableSlot.Chest,
                [
                    FireResistance,
                    ColdResistance,
                    LightningResistance,
                    Strength,
                    Agility,
                    Intelligence,
                    Vitality,
                    Spirit,
                ]
            },
            {
                EquippableSlot.Head,
                [
                    FireResistance,
                    ColdResistance,
                    LightningResistance,
                    Strength,
                    Agility,
                    Intelligence,
                    Vitality,
                    Spirit,
                ]
            },
            {
                EquippableSlot.Gloves,
                [
                    FireResistance,
                    ColdResistance,
                    LightningResistance,
                    Strength,
                    Agility,
                    Intelligence,
                    Vitality,
                    Spirit,
                ]
            },
            {
                EquippableSlot.Boots,
                [
                    FireResistance,
                    ColdResistance,
                    LightningResistance,
                    Strength,
                    Agility,
                    Intelligence,
                    Vitality,
                    Spirit,
                ]
            },
            {
                EquippableSlot.Belt,
                [
                    FireResistance,
                    ColdResistance,
                    LightningResistance,
                    Strength,
                    Agility,
                    Intelligence,
                    Vitality,
                    Spirit,
                ]
            },
            {
                EquippableSlot.Amulet,
                [
                    FireResistance,
                    ColdResistance,
                    LightningResistance,
                    Strength,
                    Agility,
                    Intelligence,
                    Vitality,
                    Spirit,
                ]
            },
            {
                EquippableSlot.Ring,
                [
                    FireResistance,
                    ColdResistance,
                    LightningResistance,
                    Strength,
                    Agility,
                    Intelligence,
                    Vitality,
                    Spirit,
                ]
            },
        }.ToImmutableDictionary();

    private static AffixFamily LesserLife =>
        new(
            AffixId.LesserLife,
            [
                new AffixTierInfo(1, 10000, 50, () => new LifeAffix(32, 40)),
                new AffixTierInfo(2, 10000, 40, () => new LifeAffix(24, 31)),
                new AffixTierInfo(3, 10000, 30, () => new LifeAffix(17, 23)),
                new AffixTierInfo(4, 10000, 20, () => new LifeAffix(11, 16)),
                new AffixTierInfo(5, 10000, 10, () => new LifeAffix(7, 11)),
                new AffixTierInfo(6, 10000, 1, () => new LifeAffix(3, 6)),
            ]
        );

    private static AffixFamily GreaterLife =>
        new(
            AffixId.GreaterLife,
            [
                new AffixTierInfo(1, 10000, 50, () => new LifeAffix(56, 70)),
                new AffixTierInfo(2, 10000, 40, () => new LifeAffix(42, 55)),
                new AffixTierInfo(3, 10000, 30, () => new LifeAffix(30, 41)),
                new AffixTierInfo(4, 10000, 20, () => new LifeAffix(20, 29)),
                new AffixTierInfo(5, 10000, 10, () => new LifeAffix(13, 19)),
                new AffixTierInfo(6, 10000, 1, () => new LifeAffix(8, 12)),
            ]
        );

    private static AffixFamily Mana =>
        new(
            AffixId.Mana,
            [
                new AffixTierInfo(1, 10000, 50, () => new ManaAffix(32, 40)),
                new AffixTierInfo(2, 10000, 40, () => new ManaAffix(24, 31)),
                new AffixTierInfo(3, 10000, 30, () => new ManaAffix(17, 23)),
                new AffixTierInfo(4, 10000, 20, () => new ManaAffix(11, 16)),
                new AffixTierInfo(5, 10000, 10, () => new ManaAffix(7, 11)),
                new AffixTierInfo(6, 10000, 1, () => new ManaAffix(3, 6)),
            ]
        );

    private static AffixFamily LifeOnKill =>
        new(
            AffixId.LifeOnKill,
            [
                new AffixTierInfo(4, 1000, 69, () => new LifeOnKillAffix(5, 6)),
                new AffixTierInfo(4, 1000, 49, () => new LifeOnKillAffix(3, 4)),
                new AffixTierInfo(5, 2000, 29, () => new LifeOnKillAffix(2)),
                new AffixTierInfo(6, 2000, 9, () => new LifeOnKillAffix(1)),
            ]
        );

    private static AffixFamily ManaOnKill =>
        new(
            AffixId.ManaOnKill,
            [
                new AffixTierInfo(4, 1000, 69, () => new ManaOnKillAffix(5, 6)),
                new AffixTierInfo(4, 1000, 49, () => new ManaOnKillAffix(3, 4)),
                new AffixTierInfo(5, 2000, 29, () => new ManaOnKillAffix(2)),
                new AffixTierInfo(6, 2000, 9, () => new ManaOnKillAffix(1)),
            ]
        );

    private static AffixFamily FireResistance =>
        new AffixFamily(
            AffixId.FireResistance,
            [
                new AffixTierInfo(1, 10000, 50, () => new FireResistanceAffix(41, 45)),
                new AffixTierInfo(2, 10000, 50, () => new FireResistanceAffix(35, 40)),
                new AffixTierInfo(3, 10000, 40, () => new FireResistanceAffix(25, 30)),
                new AffixTierInfo(4, 10000, 30, () => new FireResistanceAffix(21, 25)),
                new AffixTierInfo(5, 10000, 20, () => new FireResistanceAffix(15, 20)),
                new AffixTierInfo(6, 10000, 10, () => new FireResistanceAffix(11, 15)),
                new AffixTierInfo(7, 10000, 1, () => new FireResistanceAffix(6, 10)),
            ]
        );

    private static AffixFamily ColdResistance =>
        new AffixFamily(
            AffixId.ColdResistance,
            [
                new AffixTierInfo(1, 10000, 50, () => new ColdResistanceAffix(41, 45)),
                new AffixTierInfo(2, 10000, 50, () => new ColdResistanceAffix(35, 40)),
                new AffixTierInfo(3, 10000, 40, () => new ColdResistanceAffix(25, 30)),
                new AffixTierInfo(4, 10000, 30, () => new ColdResistanceAffix(21, 25)),
                new AffixTierInfo(5, 10000, 20, () => new ColdResistanceAffix(15, 20)),
                new AffixTierInfo(6, 10000, 10, () => new ColdResistanceAffix(11, 15)),
                new AffixTierInfo(7, 10000, 1, () => new ColdResistanceAffix(6, 10)),
            ]
        );

    private static AffixFamily LightningResistance =>
        new AffixFamily(
            AffixId.LightningResistance,
            [
                new AffixTierInfo(1, 10000, 50, () => new LightningResistanceAffix(41, 45)),
                new AffixTierInfo(2, 10000, 50, () => new LightningResistanceAffix(35, 40)),
                new AffixTierInfo(3, 10000, 40, () => new LightningResistanceAffix(25, 30)),
                new AffixTierInfo(4, 10000, 30, () => new LightningResistanceAffix(21, 25)),
                new AffixTierInfo(5, 10000, 20, () => new LightningResistanceAffix(15, 20)),
                new AffixTierInfo(6, 10000, 10, () => new LightningResistanceAffix(11, 15)),
                new AffixTierInfo(7, 10000, 1, () => new LightningResistanceAffix(6, 10)),
            ]
        );

    private static AffixFamily Strength =>
        new AffixFamily(
            AffixId.Strength,
            [
                new AffixTierInfo(1, 10000, 50, () => new StrengthAffix(23, 30)),
                new AffixTierInfo(2, 10000, 40, () => new StrengthAffix(16, 22)),
                new AffixTierInfo(3, 10000, 30, () => new StrengthAffix(10, 15)),
                new AffixTierInfo(4, 10000, 20, () => new StrengthAffix(6, 9)),
                new AffixTierInfo(5, 10000, 10, () => new StrengthAffix(3, 5)),
                new AffixTierInfo(6, 10000, 1, () => new StrengthAffix(1, 2)),
            ]
        );

    private static AffixFamily Agility =>
        new AffixFamily(
            AffixId.Agility,
            [
                new AffixTierInfo(1, 10000, 50, () => new AgilityAffix(23, 30)),
                new AffixTierInfo(2, 10000, 40, () => new AgilityAffix(16, 22)),
                new AffixTierInfo(3, 10000, 30, () => new AgilityAffix(10, 15)),
                new AffixTierInfo(4, 10000, 20, () => new AgilityAffix(6, 9)),
                new AffixTierInfo(5, 10000, 10, () => new AgilityAffix(3, 5)),
                new AffixTierInfo(6, 10000, 1, () => new AgilityAffix(1, 2)),
            ]
        );

    private static AffixFamily Intelligence =>
        new AffixFamily(
            AffixId.Intelligence,
            [
                new AffixTierInfo(1, 10000, 50, () => new IntelligenceAffix(23, 30)),
                new AffixTierInfo(2, 10000, 40, () => new IntelligenceAffix(16, 22)),
                new AffixTierInfo(3, 10000, 30, () => new IntelligenceAffix(10, 15)),
                new AffixTierInfo(4, 10000, 20, () => new IntelligenceAffix(6, 9)),
                new AffixTierInfo(5, 10000, 10, () => new IntelligenceAffix(3, 5)),
                new AffixTierInfo(6, 10000, 1, () => new IntelligenceAffix(1, 2)),
            ]
        );

    private static AffixFamily Vitality =>
        new AffixFamily(
            AffixId.Vitality,
            [
                new AffixTierInfo(1, 10000, 50, () => new VitalityAffix(23, 30)),
                new AffixTierInfo(2, 10000, 40, () => new VitalityAffix(16, 22)),
                new AffixTierInfo(3, 10000, 30, () => new VitalityAffix(10, 15)),
                new AffixTierInfo(4, 10000, 20, () => new VitalityAffix(6, 9)),
                new AffixTierInfo(5, 10000, 10, () => new VitalityAffix(3, 5)),
                new AffixTierInfo(6, 10000, 1, () => new VitalityAffix(1, 2)),
            ]
        );

    private static AffixFamily Spirit =>
        new AffixFamily(
            AffixId.Spirit,
            [
                new AffixTierInfo(1, 10000, 50, () => new SpiritAffix(23, 30)),
                new AffixTierInfo(2, 10000, 40, () => new SpiritAffix(16, 22)),
                new AffixTierInfo(3, 10000, 30, () => new SpiritAffix(10, 15)),
                new AffixTierInfo(4, 10000, 20, () => new SpiritAffix(6, 9)),
                new AffixTierInfo(5, 10000, 10, () => new SpiritAffix(3, 5)),
                new AffixTierInfo(6, 10000, 1, () => new SpiritAffix(1, 2)),
            ]
        );
}
