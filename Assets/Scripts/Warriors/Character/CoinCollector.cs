using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public class CoinCollector : MonoBehaviour
{
    public event UnityAction Collected;

    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.TryGetComponent(out Money money))
        {
            Collected?.Invoke();
            Destroy(money.gameObject);
        }
    }
}