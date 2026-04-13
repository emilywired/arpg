using System.Collections.Generic;

public class TheOneRubyRing : RubyRing, IUnique
{
    public string UniqueName => "The One";
    public string UniqueFlavorText =>
        "One Ring to rule them all,\n"
        + "One Ring to find them,\n"
        + "One Ring to bring them all\n"
        + "and in the darkness bind them.";

    public List<Affix> UniqueAffixes { get; } =
        [
            new StrengthAffix(5, 8).RollValue(),
            new AgilityAffix(5, 8).RollValue(),
            new IntelligenceAffix(5, 8).RollValue(),
            new VitalityAffix(5, 8).RollValue(),
            new SpiritAffix(5, 8).RollValue(),
        ];

    public TheOneRubyRing()
        : base()
    {
        Rarity = Rarity.Unique;
    }
}
