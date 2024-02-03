using HealthView;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarExample : MonoBehaviour
{
    [SerializeField] private List<AbstractHealthView> _healthBars;

    [SerializeField] private int _healthValue;

    [SerializeField] private int _damageValue;
    [SerializeField] private int _healValue;

    private ExampleHealth _health;

    private void Awake()
    {
        _health = new ExampleHealth(_healthValue, _healthValue);
    }

    private void Start()
    {
        _healthBars.ForEach(bar => bar.Initialize(_health));
    }

    public void Heal()
    {
        _health.Heal(_healValue);
    }

    public void Damage()
    {
        _health.Damage(_damageValue);
    }
}
