using System.Collections.Generic;

public interface ICompositeDrawable : IDrawable
{
    IEnumerable<IDrawable> ChildDrawables { get; }
}