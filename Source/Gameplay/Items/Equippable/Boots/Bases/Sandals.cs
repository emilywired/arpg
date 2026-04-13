

public class Sandals : EquippableItem
{
    public Sandals()
        : base(
            name: "Sandals",
            rarity: Rarity.Normal,
            slot: EquippableSlot.Boots,
            level: 1,
            levelRequirement: 2,
            width: 2,
            height: 2,
            asset: Assets.Items.None_2x2
        )
    {
        BaseAffixes.Add(new MovementSpeedAffix(5));
    }
}
