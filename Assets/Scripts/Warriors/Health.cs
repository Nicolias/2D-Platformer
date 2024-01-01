using System;

public class Health
{
    private int _value;

    public Health(int value)
    {
        _value = value;
    }

    public int Value => _value;

    public event Action Dieing;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new InvalidOperationException();

        _value -= damage;

        if (_value <= 0)
        {
            _value = 0;
            Dieing?.Invoke();
        }
    }
}