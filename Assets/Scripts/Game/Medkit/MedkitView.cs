using CharacterSystem;
using System;
using UnityEngine;

public class MedkitView : MonoBehaviour
{
    [SerializeField] private int _healValue;

    private Medkit _model;

    public event Action PickedUp;

    public void Initialize(Health health)
    {
        _model = new Medkit(health, _healValue);
    }

    public void PickUp()
    {
        _model.Heal();
        PickedUp?.Invoke();
    }
}
