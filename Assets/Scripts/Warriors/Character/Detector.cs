using UnityEngine;
using UnityEngine.Events;

namespace CharacterSystem
{
    public class Detector : AbstractDetector
    {
        public override event UnityAction<Health> Detected;
        public event UnityAction Lost;

        protected override void TryDetectHealth(Collider2D collision)
        {
            Enemy.Enemy enemy = collision.GetComponentInParent<Enemy.Enemy>();

            if (enemy != null)
                Detected?.Invoke(enemy.AttackAndHealth.Health);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<Enemy.Enemy>() != null)
                Lost?.Invoke();
        }
    }
}