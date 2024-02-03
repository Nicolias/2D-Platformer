using System;

public class ExampleHealth : IHealthChangeable
{
    private int _healthValue1;
    private int _healthValue2;

    public ExampleHealth(int healthValue1, int healthValue2)
    {
        _healthValue1 = healthValue1;
        _healthValue2 = healthValue2;
    }

    public float MaxValue => throw new NotImplementedException();

    public event Action<float> Changed;

    internal void Heal(int healValue)
    {
        throw new NotImplementedException();
    }

    internal void Damage(int damageValue)
    {
        throw new NotImplementedException();
    }
}