using System.Collections.Generic;
using System.Linq;

public enum DamageType
{
    Physical,
    Fire,
    Cold,
    Lightning,
}

public class DamagePacket
{
    public Dictionary<DamageType, double> Values { get; } = [];

    public double Get(DamageType type) => Values.GetValueOrDefault(type);

    public void Add(DamagePacket other)
    {
        foreach (KeyValuePair<DamageType, double> kvp in other.Values)
        {
            Values[kvp.Key] = Get(kvp.Key) + kvp.Value;
        }
    }

    public void Scale(double multiplier)
    {
        foreach (DamageType type in Values.Keys.ToList())
            Scale(type, multiplier);
    }

    public void Scale(DamageType type, double multiplier)
    {
        if (Values.ContainsKey(type))
        {
            Values[type] *= multiplier;
        }
    }
}
