public class TransmutationShard : MaterialItem
{
    public TransmutationShard(int quantity = 1)
        : base(
            name: "Transmutation Shard",
            rarity: Rarity.Magic,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Upgrades a magic item to rare,\nadding random modifiers",
            maxStackQuantity: 20,
            stackQuantity: quantity
        ) { }
}
