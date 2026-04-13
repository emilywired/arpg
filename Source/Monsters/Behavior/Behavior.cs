public class Behavior(Monster monster) : IUpdateable
{
    protected Monster monster = monster;

    public virtual void Update(float dt)
    {
    }
}