using System;
using UnityEngine;

public class Health : IHealthChangeable
{
    private float _value;

    public Health(float maxHealth)
    {
        if (maxHealth <= 0)
            throw new ArgumentOutOfRangeException();

        _value = maxHealth;
        MaxValue = maxHealth;
    }

    public float Value => _value;

    public float MaxValue { get; }

    public event Action<float> Changed;
    public event Action Over;

    public void Damage(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        _value -= value;

        OnHealthChanged();

        if (_value == 0)
            Over?.Invoke();
    }

    public void Heal(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        _value += value;

        OnHealthChanged();
    }

    private void OnHealthChanged()
    {
        _value = Mathf.Clamp(_value, 0, MaxValue);

        Changed?.Invoke(_value);
    }
}