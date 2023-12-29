using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyDetector : MonoBehaviour
{
    public event UnityAction PlayerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            PlayerDetected?.Invoke();
    }
}