using System;

public class ActorBaseStats : IUpdateable
{
    public double Speed { get; set; }
    public ReactiveProperty<double> Health { get; } = new(default);
    public ReactiveProperty<double> MaxHealth { get; } = new(default);
    public double HealthRegen { get; set; }
    public double HealthDegen { get; set; }
    public ReactiveProperty<double> Mana { get; set; } = new(default);
    public ReactiveProperty<double> MaxMana { get; set; } = new(default);
    public double ManaRegen { get; set; }
    public double ManaDegen { get; set; }
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

    private const double TICK_TIME = 0.1d;
    private Actor actor;
    private double regenTimer = 0f;

    public ActorBaseStats(
        Actor _actor,
        double speed,
        double health,
        double mana = 0,
        double healthRegen = 0,
        double manaRegen = 0,
        double evasion = 0,
        double armor = 0,
        double strength = 10,
        double agility = 10,
        double intelligence = 10,
        double vitality = 10,
        double spirit = 10
    )
    {
        actor = _actor;
        Speed = speed;
        Health.Value = MaxHealth.Value = health;
        Mana.Value = MaxMana.Value = mana;
        HealthRegen = healthRegen;
        ManaRegen = manaRegen;
        HealthDegen = 0;
        ManaDegen = 0;
        Evasion = evasion;
        Armor = armor;
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Vitality = vitality;
        Spirit = spirit;
    }

    public void Update(float dt)
    {
        regenTimer += dt;
        if (regenTimer >= TICK_TIME)
        {
            double netHealthChange = (HealthRegen - HealthDegen) * TICK_TIME;
            double netManaChange = (ManaRegen - ManaDegen) * TICK_TIME;

            if (netHealthChange < 0)
            {
                actor.TakeDamage(-netHealthChange);
            }
            else
            {
                OffsetHealth(netHealthChange);
            }

            OffsetMana(netManaChange);

            regenTimer -= TICK_TIME;
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

    public void AddHealthDegen(double damagePerSecond)
    {
        HealthDegen += damagePerSecond;
    }

    public void SubtractHealthDegen(double damagePerSecond)
    {
        HealthDegen -= damagePerSecond;
    }
}
