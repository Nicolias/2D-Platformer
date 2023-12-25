using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Detector : MonoBehaviour
    {
        public event UnityAction PlayerDetected;
        public event UnityAction PlayerLost;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerDetected?.Invoke();
        }
    }
}