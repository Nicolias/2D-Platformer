using Character;
using UnityEngine;

namespace Enemy
{
    public class Detector : AbstractDetector
    {
        protected override bool TryDetecteHealth(Collider2D collision, out Health health)
        {
            CharacterView character = collision.GetComponentInParent<CharacterView>();

            if (character != null)
            {
                health = character.Model.Health;
                return true;
            }

            health = null;
            return false;
        }
    }
}