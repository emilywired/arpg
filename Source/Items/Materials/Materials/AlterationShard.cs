public class AlterationShard : MaterialItem
{
    public AlterationShard(int quantity = 1)
        : base(
            name: "Alteration Shard",
            rarity: Rarity.Magic,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Rerolls the modifiers of a magic item",
            maxStackQuantity: 20,
            stackQuantity: quantity
        ) { }
}
