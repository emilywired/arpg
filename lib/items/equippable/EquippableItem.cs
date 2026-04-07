using System;
using System.Collections.Generic;
using System.Linq;
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

        List<Affix> affixes = [.. BaseAffixes, .. ImplicitAffixes, .. Prefixes, .. Suffixes];

        foreach (Affix affix in affixes)
        {
            affix.Apply(player, affix.RolledValue);
        }

        IsEquipped = true;
    }

    public void Unequip(Player player)
    {
        if (!IsEquipped)
            throw new SystemException("Item already unequipped");

        List<Affix> affixes = [.. BaseAffixes, .. ImplicitAffixes, .. Prefixes, .. Suffixes];

        foreach (Affix affix in affixes)
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
            BaseAffixes = [new MovementSpeedAffix(69)];
        }
        return this;
    }

    private EquippableItem RollAffixes()
    {
        Random rng = new();

        // filter valid affixes and their tiers based on ilvl
        var prefixPool = GlobalAffixes.Prefixes[Slot];
        var suffixPool = GlobalAffixes.Suffixes[Slot];

        // decide n amount of prefixes, same for suffixes
        // select n affix families
        // roll tier


        return this;
    }
}
