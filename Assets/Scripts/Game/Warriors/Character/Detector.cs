using UnityEngine;

namespace Character
{
    public class Detector : AbstractDetector
    {
        protected override bool TryDetecteHealth(Collider2D collision, out Health health)
        {
            Enemy.Enemy enemy = collision.GetComponentInParent<Enemy.Enemy>();

            if (enemy == null)
            {
                health = null;
                return false;
            }

            health = collision.GetComponentInParent<Enemy.Enemy>().AttackAndHealth.Health;
            return true;
        }
    }
}