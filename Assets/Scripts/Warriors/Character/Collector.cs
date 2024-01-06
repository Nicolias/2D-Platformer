using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Collector : MonoBehaviour
{
    public event UnityAction Collected;

    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.TryGetComponent(out Money money))
        {
            Collected?.Invoke();
            Destroy(money.gameObject);
        }

        if(collision.TryGetComponent(out MedkitView medkitView))
        {
            medkitView.PickUp();
        }
    }
}