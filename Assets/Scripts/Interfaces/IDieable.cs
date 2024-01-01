using UnityEngine.Events;

public interface IDieable
{
    public MoveAnimation MoveAnimation { get; }

    public event UnityAction Died;

    public void Die();
}