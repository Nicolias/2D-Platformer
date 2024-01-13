using Character;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class AbstractDetector : MonoBehaviour
{
    public event UnityAction<Health> Detected;
    public event UnityAction Lost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryDetecteHealth(collision, out Health health))
            Detected?.Invoke(health);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TryDetecteHealth(collision, out Health health))
            Lost?.Invoke();
    }

    protected abstract bool TryDetecteHealth(Collider2D collision, out Health health);
}