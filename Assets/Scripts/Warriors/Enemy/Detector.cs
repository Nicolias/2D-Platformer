using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Detector : MonoBehaviour
    {
        public event UnityAction PlayerDetected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out MoneyCollector character))
                PlayerDetected?.Invoke();
        }
    }
}