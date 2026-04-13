using System;

public class Cooldown(float duration)
{
    public float Duration = duration;

    private DateTime _lastCastTime = DateTime.MinValue;

    public void StartCooldown()
    {
        _lastCastTime = DateTime.Now;
    }

    public bool CanCast()
    {
        return (DateTime.Now - _lastCastTime).TotalSeconds >= Duration;
    }

    public float GetRemainingDuration()
    {
        double elapsed = (DateTime.Now - _lastCastTime).TotalSeconds;
        return (float)Math.Max(0, Duration - elapsed);
    }
}
