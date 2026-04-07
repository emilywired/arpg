using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework.Graphics;

public class EquippableItem : Item, IEquippable
{
    public int Level { get; private set; }
    public int LevelRequirement { get; private set; }
    public bool IsEquipped { get; protected set; } = false;
    public EquippableSlot Slot { get; private set; }
    public List<Affix> BaseAffixes { get; private set; } = [];
    public List<Affix> ImplicitAffixes { get; private set; } = [];
    public List<Affix> Prefixes { get; private set; } = [];
    public List<Affix> Suffixes { get; private set; } = [];

    public EquippableItem(
        string name,
        Rarity rarity,
        int level,
        int levelRequirement,
        EquippableSlot slot,
        int width,
        int height,
        Asset asset
    )
        : base(name, rarity, width, height, asset)
    {
        Slot = slot;
        Level = level;
        LevelRequirement = LevelRequirement;
    }

    public void Equip(Player player)
    {
        if (IsEquipped)
            throw new SystemException("Item already equipped");

        foreach (Affix affix in BaseAffixes)
        {
            affix.Apply(player, affix.RolledValue);
        }

        foreach (Affix affix in ImplicitAffixes)
        {
            affix.Apply(player, affix.RolledValue);
        }

        foreach (Affix affix in Prefixes)
        {
            affix.Apply(player, affix.RolledValue);
        }

        foreach (Affix affix in Suffixes)
        {
            affix.Apply(player, affix.RolledValue);
        }

        IsEquipped = true;
    }

    public void Unequip(Player player)
    {
        if (!IsEquipped)
            throw new SystemException("Item already unequipped");

        foreach (Affix affix in BaseAffixes)
        {
            affix.Apply(player, -(affix.RolledValue));
        }

        foreach (Affix affix in ImplicitAffixes)
        {
            affix.Apply(player, -(affix.RolledValue));
        }

        foreach (Affix affix in Prefixes)
        {
            affix.Apply(player, -(affix.RolledValue));
        }

        foreach (Affix affix in Suffixes)
        {
            affix.Apply(player, -(affix.RolledValue));
        }

        IsEquipped = false;
    }

    public EquippableItem ToMagic()
    {
        if (Rarity != Rarity.Normal)
            return this;

        RollAffixes();
        Rarity = Rarity.Magic;

        return this;
    }

    public EquippableItem ToRare()
    {
        if (Rarity != Rarity.Normal)
            return this;

        RollAffixes();
        Rarity = Rarity.Rare;

        return this;
    }

    public EquippableItem Corrupt()
    {
        var rng = new Random();
        int roll = rng.Next(0, 3);
        if (roll == 0)
        {
            ToRare();
        }
        else if (roll == 1 || roll == 2)
        {
            // TODO: affix from pool, specific for each gear type
            BaseAffixes = [new MovementSpeedAffix(10)];
        }
        return this;
    }

    private EquippableItem RollAffixes()
    {
        if (IsCorrupted)
            return this;
        IsCorrupted = true;

        var rng = new Random();
        int roll = rng.Next(0, 3);
        if (roll == 0)
        {
            ToRare();
        }
        else if (roll == 1 || roll == 2)
        {
            // TODO: affix from pool, specific for each gear type
            ImplicitAffixes = [new MovementSpeedAffix(11)];
        }
        return this;
    }
}
