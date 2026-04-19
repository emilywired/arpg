using System;
using System.Collections.Generic;
using System.Linq;

public class Resistances(
    double fire = 0,
    double cold = 0,
    double lightning = 0,
    double physical = 0
)
{
    private double ELEMENTAL_RESISTANCE_CAP = 90;
    private double PHYSICAL_RESISTANCE_CAP = 50;

    public Dictionary<DamageType, double> Values = new()
    {
        { DamageType.Fire, fire },
        { DamageType.Cold, cold },
        { DamageType.Lightning, lightning },
        { DamageType.Physical, physical },
        { DamageType.Static, 0 },
    };

    public Dictionary<DamageType, double> MaxValues = new()
    {
        { DamageType.Fire, 75 },
        { DamageType.Cold, 75 },
        { DamageType.Lightning, 75 },
        { DamageType.Physical, 50 },
        { DamageType.Static, 0 },
    };

    public double GetCappedResistance(DamageType damageType)
    {
        double maxResistance = 0;

        switch (damageType)
        {
            case DamageType.Physical:
                maxResistance = Math.Min(MaxValues[damageType], PHYSICAL_RESISTANCE_CAP);
                break;
            case DamageType.Static:
                break;
            default:
                maxResistance = Math.Min(MaxValues[damageType], ELEMENTAL_RESISTANCE_CAP);
                break;
        }

        return Math.Min(Values[damageType], maxResistance);
    }
}

public class ActorStats
{
    public Actor Actor;
    public double Speed { get; set; }
    public ReactiveProperty<double> Health { get; } = new(default);
    public ReactiveProperty<double> MaxHealth { get; } = new(default);
    public ReactiveProperty<double> Mana { get; } = new(default);
    public ReactiveProperty<double> MaxMana { get; } = new(default);
    public Dictionary<object, DamagePacket> HealthRateSources { get; private set; } = [];
    public Dictionary<object, double> ManaRateSources { get; private set; } = [];
    public double Evasion { get; set; }
    public double Armor { get; set; }
    public double Strength { get; set; }
    public double Agility { get; set; }
    public double Intelligence { get; set; }
    public double Vitality { get; set; }
    public double Spirit { get; set; }
    public Resistances Resistances = new();

    public ActorStats(
        Actor _actor,
        double speed,
        double health,
        double mana = 0,
        double healthRate = 0,
        double manaRate = 0,
        double evasion = 0,
        double armor = 0,
        double strength = 10,
        double agility = 10,
        double intelligence = 10,
        double vitality = 10,
        double spirit = 10
    )
    {
        Actor = _actor;
        Speed = speed;
        Health.Value = MaxHealth.Value = health;
        Mana.Value = MaxMana.Value = mana;
        Evasion = evasion;
        Armor = armor;
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Vitality = vitality;
        Spirit = spirit;

        if (healthRate != 0)
        {
            AddHealthRate(this, new(@static: healthRate));
        }

        if (manaRate != 0)
        {
            AddManaRate(this, manaRate);
        }
    }

    public void OffsetHealth(double amount)
    {
        Health.Value = Math.Clamp(Health.Value + amount, 0, MaxHealth.Value);
    }

    public void OffsetMana(double amount)
    {
        Mana.Value = Math.Clamp(Mana.Value + amount, 0, MaxMana.Value);
    }

    public void AddHealthRate(object source, DamagePacket rate)
    {
        HealthRateSources[source] = rate;
    }

    public void RemoveHealthRate(object source)
    {
        _ = HealthRateSources.Remove(source);
    }

    public void AddManaRate(object source, double value)
    {
        ManaRateSources[source] = value;
    }

    public void RemoveManaRate(object source)
    {
        _ = ManaRateSources.Remove(source);
    }

    public double GetManaRateDelta(float dt)
    {
        double netManaChange = ManaRateSources.Values.Sum();
        double manaChange = netManaChange * dt;
        return manaChange;
    }
}
