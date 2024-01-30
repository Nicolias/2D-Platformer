using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class AbstractDetector : MonoBehaviour
{
    public event Action<IDamagable> Detected;
    public event Action Lost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryDetecteHealth(collision, out IDamagable damagable))
            Detected?.Invoke(damagable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TryDetecteHealth(collision, out IDamagable damagable))
            Lost?.Invoke();
    }

    protected abstract bool TryDetecteHealth(Collider2D collision, out IDamagable damagable);
}