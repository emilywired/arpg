using System;

public class Cooldown(float duration)
{
    public float Duration = duration;

    private DateTime lastCastTime = DateTime.MinValue;

    public void StartCooldown()
    {
        lastCastTime = DateTime.Now;
    }

    public bool CanCast()
    {
        return (DateTime.Now - lastCastTime).TotalSeconds >= Duration;
    }

    public float GetRemainingDuration()
    {
        double elapsed = (DateTime.Now - lastCastTime).TotalSeconds;
        return (float)Math.Max(0, Duration - elapsed);
    }
}
