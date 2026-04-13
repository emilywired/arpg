

public class Hood : EquippableItem
{
    public Hood()
        : base(
            name: "Hood",
            rarity: Rarity.Normal,
            slot: EquippableSlot.Head,
            level: 1,
            levelRequirement: 1,
            width: 2,
            height: 2,
            asset: Assets.Items.None_2x2
        )
    {
        BaseAffixes.Add(new EvasionAffix(6, 10));
    }
}
