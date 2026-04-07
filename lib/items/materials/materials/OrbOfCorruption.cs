using arpg;
using Microsoft.Xna.Framework;

public class OrbOfCorruption : MaterialItem, IUsableOnItem
{
    public OrbOfCorruption()
        : base(
            name: "Orb of Corruption",
            rarity: Rarity.Magic,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Unpredictably modifies an item.",
            maxStackQuantity: 50
        ) { }

    public void UseOn(Item item)
    {
        if (item is not ICorruptable corruptable)
            return;

        corruptable.Corrupt();

        StackQuantity -= 1;
        if (StackQuantity == 0)
            Game1.World.Player.Inventory.RemoveItem(this);
    }
}
