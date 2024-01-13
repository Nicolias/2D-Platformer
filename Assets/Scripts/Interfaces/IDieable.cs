using UnityEngine.Events;

public interface IDieable
{

    public event UnityAction Died;

    public void Die();
}