public class PlayerStats : ActorBaseStats
{
    public PlayerLevel Level { get; }
    public int HealthOnKill { get; set; }
    public int ManaOnKill { get; set; }

    public PlayerStats(
        IActor actor,
        double speed,
        double health,
        double mana = 0,
        double healthRegen = 0,
        double manaRegen = 0,
        int healthOnKill = 0,
        int manaOnKill = 0,
        double evasion = 0,
        double armor = 0,
        double strength = 10,
        double agility = 10,
        double intelligence = 10,
        double vitality = 10,
        double spirit = 10
    )
        : base(
            actor,
            speed,
            health,
            mana,
            healthRegen,
            manaRegen,
            evasion,
            armor,
            strength,
            agility,
            intelligence,
            vitality,
            spirit
        )
    {
        Level = new();
        HealthOnKill = healthOnKill;
        ManaOnKill = manaOnKill;
    }
}
