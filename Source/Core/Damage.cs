using System;
using System.Collections.Generic;
using System.Linq;

public enum DamageType
{
    Physical,
    Fire,
    Cold,
    Lightning,
    Unmodifiable,
}

public static class DamageCalculations
{
    public static DamagePacket ApplyResistances(DamagePacket damage, ActorStats stats)
    {
        DamagePacket copy = damage.Copy();

        double fireResistance = Math.Min(stats.FireResistance, stats.MaxFireResistance);
        copy = copy.Scale(DamageType.Fire, (100 - fireResistance) / 100);

        double coldResistance = Math.Min(stats.ColdResistance, stats.MaxColdResistance);
        copy = copy.Scale(DamageType.Fire, (100 - coldResistance) / 100);

        double lightningResistance = Math.Min(stats.LightningResistance, stats.LightningResistance);
        copy = copy.Scale(DamageType.Fire, (100 - lightningResistance) / 100);

        return copy;
    }
}

public class DamagePacket
{
    public Dictionary<DamageType, double> Types { get; } = [];

    public DamagePacket(
        double physical = 0,
        double fire = 0,
        double cold = 0,
        double lightning = 0,
        double unmodifiable = 0
    )
    {
        Types[DamageType.Physical] = physical;
        Types[DamageType.Fire] = fire;
        Types[DamageType.Cold] = cold;
        Types[DamageType.Lightning] = lightning;
        Types[DamageType.Unmodifiable] = unmodifiable;
    }

    private DamagePacket(Dictionary<DamageType, double> types)
    {
        Types = new(types);
    }

    public DamagePacket Copy() => new(Types);

    public double Get(DamageType type) => Types.GetValueOrDefault(type);

    public void Add(DamagePacket other)
    {
        foreach (KeyValuePair<DamageType, double> kvp in other.Types)
        {
            Types[kvp.Key] = Get(kvp.Key) + kvp.Value;
        }
    }

    public double Sum() => Types.Values.Sum();

    public DamagePacket Scale(double multiplier)
    {
        DamagePacket copy = Copy();
        foreach (DamageType type in copy.Types.Keys)
            copy.Types[type] *= multiplier;

        return copy;
    }

    public DamagePacket Scale(DamageType type, double multiplier)
    {
        DamagePacket copy = Copy();
        copy.Types[type] *= multiplier;
        return copy;
    }
}
