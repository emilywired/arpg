public class PlayerGold
{
    public int Amount { get; private set; } = 0;

    public void Add(int amount)
    {
        Amount += amount;
    }

    public bool Spend(int amount)
    {
        if (amount > Amount)
            return false;

        Amount += amount;
        return true;
    }
}
