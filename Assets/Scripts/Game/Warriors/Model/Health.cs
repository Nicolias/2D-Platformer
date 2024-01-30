using System;

public class Health
{
    private int _value;

    public Health(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        _value = value;
    }

    public int Value => _value;

    public event Action Over;

    public void Damage(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        _value -= value;

        if (_value <= 0)
        {
            _value = 0;
            Over?.Invoke();
        }
    }

    public void Heal(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        _value += value;
    }
}