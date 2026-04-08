using System;
using System.Collections.Generic;
using System.Linq;

public class LootSystem
{
    private Dictionary<Func<Item>, int> _lootPool = new()
    {
        { () => new OrbOfCorruption(), 1000 },
        { () => new AugmentingCore(), 100 },
        { () => new Hood(), 5000 },
        { () => new Sandals(), 5000 },
        { () => new RubyRing(), 100000 },
        { () => new SapphireRing(), 100000 },
    };

    private Dictionary<int, int> _dropCountWeights = new()
    {
        { 0, 6000 },
        { 1, 1000 },
        { 2, 0 },
        { 3, 0 },
    };

    private Dictionary<Rarity, int> _rarityWeights = new()
    {
        { Rarity.Unique, 0 },
        { Rarity.Set, 0 },
        { Rarity.Magic, 3000 },
        { Rarity.Rare, 3000 },
        { Rarity.Normal, 3000 },
    };

    public List<Item> GenerateLoot(IMonster monster, Player player)
    {
        List<Item> drops = [];

        int goldRoll = Random.Shared.Next(0, 1);
        if (goldRoll == 0)
        {
            int goldAmount = Random.Shared.Next(
                monster.Level * 10,
                monster.Level * 10 + monster.Level * 2
            );
            drops.Add(new Gold(goldAmount));
        }

        int dropCount = RandomUtils.WeightedChoice(_dropCountWeights);
        for (int i = 0; i < dropCount; i++)
        {
            Item item = RandomUtils.WeightedChoice(_lootPool).Invoke();

            if (
                item is EquippableItem equippableItem
                && item.Rarity != Rarity.Unique
                && item.Rarity != Rarity.Set
            )
                RollRarity(equippableItem);

            drops.Add(item);
        }

        return drops;
    }

    private void RollRarity(EquippableItem item)
    {
        Rarity rarity = RandomUtils.WeightedChoice(_rarityWeights);
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
