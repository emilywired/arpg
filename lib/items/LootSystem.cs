using System;
using System.Collections.Generic;
using System.Linq;

public class LootSystem
{
    private Dictionary<Func<Item>, int> lootPool = new()
    {
        { () => new OrbOfCorruption(), 1000 },
        { () => new AugmentingCore(), 100 },
        { () => new Hood(), 5000 },
        { () => new Sandals(), 5000 },
        { () => new RubyRing(), 1000 },
        { () => new SapphireRing(), 1000 },
    };
    private int _totalLootPoolWeights => lootPool.Values.Sum();

    private Dictionary<int, int> _dropCountWeights = new()
    {
        { 0, 9000 },
        { 1, 1000 },
        { 2, 150 },
        { 3, 50 },
    };
    private int _totalDropCountWeights => _dropCountWeights.Values.Sum();

    private Dictionary<Rarity, int> _rarityWeights = new()
    {
        { Rarity.Unique, 0 },
        { Rarity.Set, 0 },
        { Rarity.Magic, 10000 },
        { Rarity.Rare, 3000 },
        { Rarity.Normal, 1000 },
    };
    private int _totalRarityWeights => _rarityWeights.Values.Sum();

    private Random _random = new();

    public List<Item> GenerateLoot(IMonster monster, Player player)
    {
        List<Item> drops = [];

        int goldRoll = _random.Next(0, 20);
        if (goldRoll == 0)
        {
            int goldAmount = _random.Next(
                monster.Level * 10,
                monster.Level * 10 + monster.Level * 2
            );
            drops.Add(new Gold(goldAmount));
        }

        int dropCount = RollDropCount();
        for (int i = 0; i < dropCount; i++)
        {
            int roll = _random.Next(0, _totalDropCountWeights);
            int cumulative = 0;
            foreach (var (createItem, weight) in lootPool)
            {
                cumulative += weight;
                if (roll < cumulative)
                {
                    Item item = createItem();

                    if (
                        item.Rarity != Rarity.Unique
                        && item.Rarity != Rarity.Set
                        && item is EquippableItem equippableItem
                    )
                    {
                        RollRarity(equippableItem);
                    }
                    drops.Add(item);
                    break;
                }
            }
        }

        return drops;
    }

    private int RollDropCount()
    {
        int roll = _random.Next(0, _totalDropCountWeights);
        int cumulative = 0;
        foreach (var weight in _dropCountWeights)
        {
            cumulative += weight.Value;
            if (roll < cumulative)
                return weight.Key;
        }

        return 0;
    }

    private void RollRarity(EquippableItem item)
    {
        int roll = _random.Next(0, _totalRarityWeights);
        int cumulative = 0;
        foreach (var weight in _rarityWeights)
        {
            cumulative += weight.Value;
            if (roll < cumulative)
            {
                Rarity rarity = weight.Key;
                switch (rarity)
                {
                    case Rarity.Magic:
                        item.ToMagic();
                        break;
                    case Rarity.Rare:
                        item.ToRare();
                        break;
                }
            }
        }
    }
}
