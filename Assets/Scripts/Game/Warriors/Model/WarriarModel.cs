using UnityEngine.Events;

public abstract class WarriarModel : IToggleable
{
    protected Health Health { get; private set; }

    public WarriarModel(Health health)
    {
        Health = health;
    }

    public event UnityAction Died;

    public void Enable()
    {
        Health.Over += Die;
    }

    public void Disable()
    {
        Health.Over -= Die;
    }

    public void Damage(int value)
    {
        Health.Damage(value);
    }

    private void Die()
    {
        Disable();
        Died?.Invoke();
    }
}
