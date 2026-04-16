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
    public static void ApplyResistances(DamagePacket damage, ActorStats stats)
    {
        foreach (DamageType type in damage.Types.Keys)
        {
            ApplyResistance(type, damage, stats);
        }
    }

    private static void ApplyResistance(DamageType type, DamagePacket damage, ActorStats stats)
    {
        double resist = 0;

        switch (type)
        {
            case DamageType.Physical:
                resist = 0;
                break;
            case DamageType.Fire:
                resist = Math.Min(stats.FireResistance, stats.MaxFireResistance);
                break;
            case DamageType.Cold:
                resist = Math.Min(stats.ColdResistance, stats.MaxColdResistance);
                break;
            case DamageType.Lightning:
                resist = Math.Min(stats.LightningResistance, stats.MaxLightningResistance);
                break;
        }

        double multiplier = (100 - resist) / 100;
        damage.Scale(type, multiplier);
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
        if (physical != 0)
            Types[DamageType.Physical] = physical;

        if (fire != 0)
            Types[DamageType.Fire] = fire;

        if (cold != 0)
            Types[DamageType.Cold] = cold;

        if (lightning != 0)
            Types[DamageType.Lightning] = lightning;

        if (unmodifiable != 0)
            Types[DamageType.Unmodifiable] = unmodifiable;
    }

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
        foreach (DamageType type in Types.Keys)
            Scale(type, multiplier);

        return this;
    }

    public void Scale(DamageType type, double multiplier)
    {
        if (Types.ContainsKey(type))
        {
            Types[type] *= multiplier;
        }
    }
}
