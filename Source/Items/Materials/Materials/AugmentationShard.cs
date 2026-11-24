public class AugmentationShard : MaterialItem
{
    public AugmentationShard(int quantity = 1)
        : base(
            name: "Augmentation Shard",
            rarity: Rarity.Rare,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Adds a random modifier to a rare item",
            maxStackQuantity: 20,
            stackQuantity: quantity
        ) { }
}
