using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public class MoneyCollector : MonoBehaviour
{
    public event UnityAction MoneyCollected;

    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.TryGetComponent(out Money money))
        {
            MoneyCollected?.Invoke();
            Destroy(money.gameObject);
        }
    }
}