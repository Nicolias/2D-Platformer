using CharacterNamespace;
using UnityEngine;

namespace EnemyNamespace
{
    public class Detector : AbstractDetector
    {
        protected override bool TryDetecteHealth(Collider2D collision, out IDamagable warriar)
        {
            warriar = collision.GetComponentInParent<CharacterView>();

            return warriar != null;
        }
    }
}