using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class AbstractDetector : MonoBehaviour
{
    public abstract event UnityAction<Health> Detected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDetectHealth(collision);
    }

    protected abstract void TryDetectHealth(Collider2D collision);
}