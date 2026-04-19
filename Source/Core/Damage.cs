using System;
using System.Collections.Generic;
using System.Linq;

public enum DamageType
{
    Physical,
    Fire,
    Cold,
    Lightning,
    Static,
}

public static class DamageCalculations
{
    public static DamagePacket WithResistances(DamagePacket damage, ActorStats stats)
    {
        foreach (DamageType damageType in Enum.GetValues<DamageType>())
        {
            double cappedResistance = stats.Resistances.GetCappedResistance(damageType);
            double resistanceReduction = (100 - cappedResistance) / 100;
            damage = damage.Scale(damageType, resistanceReduction);
        }

        return damage;
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
        double @static = 0
    )
    {
        Types[DamageType.Physical] = physical;
        Types[DamageType.Fire] = fire;
        Types[DamageType.Cold] = cold;
        Types[DamageType.Lightning] = lightning;
        Types[DamageType.Static] = @static;
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
