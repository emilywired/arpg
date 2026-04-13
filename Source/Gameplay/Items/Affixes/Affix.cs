using System;

public enum AffixType
{
    Base, // Tied to the base e.g. Armor, Evasion, Charm slots on a Belt
    Prefix, // 1-2 on Magic, 2-3 on Rare
    Suffix, // 1-2 on Magic, 2-3 on Rare
    Implicit, // Can roll on Normal items
}

public abstract class Affix
{
    public int MinRange { get; private set; }
    public int MaxRange { get; private set; }
    public int RolledValue { get; private set; }

    private static Random _rng = new();

    public Affix(int minRange, int? maxRange = null)
    {
        MinRange = minRange;
        MaxRange = maxRange ?? minRange;
        RollValue();
    }

    public Affix RollValue()
    {
        RolledValue = _rng.Next(MinRange, MaxRange + 1);
        return this;
    }

    public abstract void Apply(Player player, int value);

    public new abstract string ToString();

    public virtual string ToStringBase()
    {
        return ToString();
    }
}
