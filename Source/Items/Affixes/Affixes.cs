public class LifeAffix : Affix
{
    public LifeAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.MaxHealth.Value += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Life";
    }
}

public class ManaAffix : Affix
{
    public ManaAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.MaxMana.Value += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Mana";
    }
}

public class EvasionAffix : Affix
{
    public EvasionAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.Evasion += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Evasion";
    }

    public override string ToStringBase()
    {
        return $"Evasion:{RolledValue}";
    }
}

public class ArmorAffix : Affix
{
    public ArmorAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.Armor += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Armor";
    }

    public override string ToStringBase()
    {
        return $"Armor:{RolledValue}";
    }
}

public class MovementSpeedAffix : Affix
{
    public MovementSpeedAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.Speed += value;
    }

    public override string ToString()
    {
        return $"{RolledValue}% Increased Movement Speed";
    }
}

public class LifeOnKillAffix : Affix
{
    public LifeOnKillAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).HealthOnKill += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Life on Kill";
    }
}

public class ManaOnKillAffix : Affix
{
    public ManaOnKillAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).ManaOnKill += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Mana on Kill";
    }
}

public class FireResistanceAffix : Affix
{
    public FireResistanceAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).FireResistance += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue}% To Fire Resistance";
    }
}

public class ColdResistanceAffix : Affix
{
    public ColdResistanceAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).ColdResistance += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue}% To Cold Resistance";
    }
}

public class LightningResistanceAffix : Affix
{
    public LightningResistanceAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).LightningResistance += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue}% To Lightning Resistance";
    }
}

public class StrengthAffix : Affix
{
    public StrengthAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).Strength += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Strength";
    }
}

public class AgilityAffix : Affix
{
    public AgilityAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).Agility += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Agility";
    }
}

public class IntelligenceAffix : Affix
{
    public IntelligenceAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).Intelligence += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Intelligence";
    }
}

public class VitalityAffix : Affix
{
    public VitalityAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).Vitality += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Vitality";
    }
}

public class SpiritAffix : Affix
{
    public SpiritAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).Spirit += value;
    }

    public override string ToString()
    {
        return $"+{RolledValue} to Spirit";
    }
}
