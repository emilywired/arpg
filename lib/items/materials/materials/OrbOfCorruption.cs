using arpg;

public class OrbOfCorruption : MaterialItem
{
    public OrbOfCorruption(int quantity = 1)
        : base(
            name: "Orb of Corruption",
            rarity: Rarity.Magic,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Unpredictably modifies an item.",
            maxStackQuantity: 50,
            stackQuantity: quantity
        ) { }
}
