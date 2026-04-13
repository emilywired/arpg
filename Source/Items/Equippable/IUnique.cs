using System.Collections.Generic;

public interface IUnique
{
    string UniqueName { get; }
    string UniqueFlavorText { get; }
    List<Affix> UniqueAffixes { get; }
}
