public interface ICorruptable
{
    void Corrupt();
}

public static class ICorruptableExtensions
{
    public static T Corrupted<T>(this T self)
        where T : ICorruptable
    {
        self.Corrupt();
        return self;
    }
}