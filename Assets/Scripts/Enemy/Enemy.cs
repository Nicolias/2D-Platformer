using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(MoveAnimation))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Detector _detector;
        [SerializeField] private StateMachine _movementStateMachine;
        [SerializeField] private Movement _movement;

        private MoveAnimation _moveAnimation;

        public Detector Detector => _detector;

        public void Initialize(Character character)
        {
            _moveAnimation = GetComponent<MoveAnimation>();

            _movement.Initialize(transform);
            _movementStateMachine.Initialize(character, _movement, _detector);
            _moveAnimation.Initialize(_movement);
        }
    }
}