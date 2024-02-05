public abstract class AbstractInput : IFixedUpdateable, IUpdateable
{
    void IFixedUpdateable.Update(float timeBetweenFrame)
    {
        FixedUpdate(timeBetweenFrame);
    }

    void IUpdateable.Update(float timeBetweenFrame)
    {
        Update(timeBetweenFrame);
    }

    protected virtual void FixedUpdate(float timeBetweenFrame){}
    protected virtual void Update(float timeBetweenFrame){}
}
