public abstract class AbstractInput : IFixedUpdateable
{
    void IFixedUpdateable.Update(float timeBetweenFrame)
    {
        FixedUpdate(timeBetweenFrame);
    }

    protected abstract void FixedUpdate(float timeBetweenFrame);
}
