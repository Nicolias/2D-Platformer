using EnemyNamespace;
using UnityEngine;

namespace CharacterNamespace
{
    public class Detector : AbstractDetector
    {
        protected override bool TryDetecteHealth(Collider2D collision, out IDamagable warriar)
        {
            warriar = collision.GetComponentInParent<EnemyView>();

            return warriar != null;
        }
    }
}