using CharacterSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class Detector : AbstractDetector
    {
        public override event UnityAction<Health> Detected;

        protected override void TryDetectHealth(Collider2D collision)
        {
            Character character = collision.GetComponentInParent<Character>();

            if (character != null)
                Detected?.Invoke(character.AttackAndHealth.Health);
        }
    }
}