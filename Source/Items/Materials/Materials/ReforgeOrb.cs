public class ReforgeOrb : MaterialItem
{
    public ReforgeOrb(int quantity = 1)
        : base(
            name: "Reforge Orb",
            rarity: Rarity.Rare,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Rerolls the modifiers of a rare item",
            maxStackQuantity: 20,
            stackQuantity: quantity
        ) { }
}
