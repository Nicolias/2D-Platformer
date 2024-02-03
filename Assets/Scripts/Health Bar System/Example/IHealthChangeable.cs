using System;
using UnityEngine;

public interface IHealthChangeable
{
    public float MaxValue { get; }

    public event Action<float> Changed;
}