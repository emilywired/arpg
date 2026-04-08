using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        RollAffixGroup(GlobalAffixes.Prefixes[Slot], Random.Shared.Next(1, 3));
        RollAffixGroup(GlobalAffixes.Suffixes[Slot], Random.Shared.Next(1, 3));
        Rarity = Rarity.Magic;

        return this;
    }

    public EquippableItem ToRare()
    {
        RollAffixGroup(GlobalAffixes.Prefixes[Slot], Random.Shared.Next(2, 4));
        RollAffixGroup(GlobalAffixes.Suffixes[Slot], Random.Shared.Next(2, 4));
        Rarity = Rarity.Rare;

        return this;
    }

    public EquippableItem Corrupt()
    {
        if (IsCorrupted)
            return this;

        int roll = Random.Shared.Next(0, 3);
        if (roll == 0)
        {
            ToRare();
        }
        else if (roll == 1 || roll == 2)
        {
            var affixPool = GlobalAffixes
                .CorruptedImplicits[Slot]
                .Where(affixData => affixData.RequiredItemLevel <= Level);

            var weights = affixPool.Select(affixData => affixData.Weight);
            var affix = RandomUtils.WeightedChoice(affixPool, weights).CreateAffix();
            ImplicitAffixes = [affix];
        }

        IsCorrupted = true;
        return this;
    }

    private void RollAffixGroup(IEnumerable<AffixFamily> sourcePool, int amount)
    {
        var affixFamilyPool = sourcePool
            .Select(family =>
                family with
                {
                    Tiers = family
                        .Tiers.Where(tier => tier.RequiredItemLevel <= Level)
                        .ToImmutableArray(),
                }
            )
            .Where(family => family.Tiers.Count() > 0)
            .ToList();

        int totalPrefixWeight = affixFamilyPool.Sum(AffixFamily => AffixFamily.TotalWeight);

        for (int i = 0; i < amount; i++)
        {
            if (affixFamilyPool.Count() == 0)
            {
                break;
            }

            var rolledFamily = RandomUtils.WeightedChoice(
                affixFamilyPool,
                affixFamilyPool.Select(affixFamily => affixFamily.TotalWeight)
            );

            var rolledAffix = RandomUtils.WeightedChoice(
                rolledFamily.Tiers,
                rolledFamily.Tiers.Select(tier => tier.Weight)
            );

            Prefixes.Add(rolledAffix.CreateAffix());

            affixFamilyPool.Remove(rolledFamily);
        }
    }
}
