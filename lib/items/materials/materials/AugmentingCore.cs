using arpg;

public class AugmentingCore : MaterialItem
{
    public AugmentingCore()
        : base(
            name: "Augmenting Core",
            rarity: Rarity.Magic,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1,
            description: "Use to attempt to upgrade an \nexisting modifier on a Magic or\nRare item by one Tier.",
            maxStackQuantity: 20
        ) { }
}
