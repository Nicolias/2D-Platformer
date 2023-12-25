using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(MoveAnimation))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Detector _detector;
        [SerializeField] private MovmentStateMachine _movementStateMachine;

        private MoveAnimation _moveAnimation;

        public Detector Detector => _detector;

        public void Initialize(Character character)
        {
            _moveAnimation = GetComponent<MoveAnimation>();

            _movementStateMachine.Initialize(character);
            _moveAnimation.Initialize(_movementStateMachine.CurrentMovement);
        }

        private void OnEnable()
        {
            _detector.PlayerDetected += OnPlayerDetected;
            _detector.PlayerLost += OnPlayerLost;

            _movementStateMachine.MovementChanged += OnMovementChanged;
        }

        private void OnDisable()
        {
            _detector.PlayerDetected -= OnPlayerDetected;
            _detector.PlayerLost -= OnPlayerLost;

            _movementStateMachine.MovementChanged += OnMovementChanged;
        }

        private void OnPlayerDetected()
        {
            _movementStateMachine.ChangeState<FollowState>();
            OnMovementChanged();
        }

        private void OnPlayerLost()
        {
            _movementStateMachine.ChangeState<PatrollingState>();
            OnMovementChanged();
        }

        private void OnMovementChanged()
        {
            _moveAnimation.Initialize(_movementStateMachine.CurrentMovement);
        }
    }
}