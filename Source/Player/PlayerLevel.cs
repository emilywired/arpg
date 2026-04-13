using System;

public class PlayerLevel
{
    public int Current { get; private set; }
    public int CurrentXP { get; private set; }
    public int RequiredXP { get; private set; }

    private const int LEVEL_CAP = 100;
    private const int BASE_INCREMENT = 50;
    private const float GROWTH_RATE = 2f;

    public PlayerLevel()
    {
        Current = 1;
        CurrentXP = 0;
        RequiredXP = GetRequiredXP(Current);
    }

    public void GrantXP(int amount)
    {
        if (Current >= LEVEL_CAP)
            return;

        CurrentXP += amount;
        while (CurrentXP > RequiredXP)
        {
            int leftoverXP = CurrentXP - RequiredXP;
            CurrentXP = leftoverXP;
            RequiredXP = GetRequiredXP(++Current);
        }
    }

    private int GetRequiredXP(int level)
    {
        return (int)Math.Floor(BASE_INCREMENT * Current * GROWTH_RATE);
    }
}
