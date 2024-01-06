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

    public void Damage(int value)
    {
        if (value <= 0)
            throw new InvalidOperationException();

        _value -= value;

        if (_value <= 0)
        {
            _value = 0;
            Dieing?.Invoke();
        }
    }

    public void Heal(int value)
    {
        if (value <= 0)
            throw new InvalidOperationException();

        _value += value;
    }
}