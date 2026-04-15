using System;
using System.Collections.Generic;
using System.Linq;

public class ActorStats
{
    public Actor Actor;
    public double Speed { get; set; }
    public ReactiveProperty<double> Health { get; } = new(default);
    public ReactiveProperty<double> MaxHealth { get; } = new(default);
    public ReactiveProperty<double> Mana { get; } = new(default);
    public ReactiveProperty<double> MaxMana { get; } = new(default);
    public Dictionary<object, double> HealthRateSources { get; private set; } = [];
    public Dictionary<object, double> ManaRateSources { get; private set; } = [];
    public double Evasion { get; set; }
    public double Armor { get; set; }
    public double Strength { get; set; }
    public double Agility { get; set; }
    public double Intelligence { get; set; }
    public double Vitality { get; set; }
    public double Spirit { get; set; }
    public double FireResistance { get; set; }
    public double ColdResistance { get; set; }
    public double LightningResistance { get; set; }

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

        AddHealthRate(this, healthRate);
        AddManaRate(this, manaRate);
    }

    public void OffsetHealth(double amount)
    {
        Health.Value = Math.Clamp(Health.Value + amount, 0, MaxHealth.Value);
    }

    public void OffsetMana(double amount)
    {
        Mana.Value = Math.Clamp(Mana.Value + amount, 0, MaxMana.Value);
    }

    public void AddHealthRate(object source, double value)
    {
        HealthRateSources[source] = value;
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

    public double GetHealthDelta(float dt)
    {
        double netHealthChange = HealthRateSources.Values.Sum();
        double healthDelta = netHealthChange * dt;
        return healthDelta;
    }

    public double GetManaDelta(float dt)
    {
        double netManaChange = ManaRateSources.Values.Sum();
        double manaChange = netManaChange * dt;
        return manaChange;
    }
}
