using UnityEngine;

public interface IDamagable : IDieable
{
    public void Damage(int value);

    public Vector2 Position { get; }
}