public class FatedOrb : MaterialItem
{
    public FatedOrb(int quantity = 1)
        : base(
            name: "Fated Orb",
            rarity: Rarity.Unique,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Adds a random fated modifier to a rare item.",
            maxStackQuantity: 10,
            stackQuantity: quantity
        ) { }
}
