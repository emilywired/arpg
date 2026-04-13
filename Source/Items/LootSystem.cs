using System;
using System.Collections.Generic;

public class LootSystem
{
    private Dictionary<Func<Item>, int> lootPool = new()
    {
        { () => new OrbOfCorruption(), 1000 },
        { () => new AugmentingCore(), 100 },
        { () => new Hood(), 5000 },
        { () => new Sandals(), 5000 },
        { () => new RubyRing(), 100000 },
        { () => new SapphireRing(), 100000 },
    };

    private Dictionary<int, int> dropCountWeights = new()
    {
        { 0, 6000 },
        { 1, 1000 },
        { 2, 0 },
        { 3, 0 },
    };

    private Dictionary<Rarity, int> rarityWeights = new()
    {
        { Rarity.Unique, 0 },
        { Rarity.Set, 0 },
        { Rarity.Magic, 3000 },
        { Rarity.Rare, 3000 },
        { Rarity.Normal, 3000 },
    };

    public List<Item> GenerateLoot(Monster monster, Player player)
    {
        int MagicFind = player.Stats.MagicFind;

        List<Item> drops = [];

        int goldRoll = Random.Shared.Next(0, 1);
        if (goldRoll == 0)
        {
            int goldAmount = Random.Shared.Next(
                monster.Level * 10,
                (monster.Level * 10) + (monster.Level * 2)
            );
            drops.Add(new Gold(goldAmount));
        }

        int dropCount = RandomUtils.WeightedChoice(dropCountWeights);
        for (int i = 0; i < dropCount; i++)
        {
            Item item = RandomUtils.WeightedChoice(lootPool).Invoke();

            if (
                item is EquippableItem equippableItem
                && item.Rarity != Rarity.Unique
                && item.Rarity != Rarity.Set
            )
            {
                RollRarity(equippableItem);
            }

            drops.Add(item);
        }

        return drops;
    }

    private void RollRarity(EquippableItem item)
    {
        Rarity rarity = RandomUtils.WeightedChoice(rarityWeights);
        switch (rarity)
        {
            case Rarity.Magic:
                _ = item.ToMagic();
                break;
            case Rarity.Rare:
                _ = item.ToRare();
                break;
        }
    }
}
