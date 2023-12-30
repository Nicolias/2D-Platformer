using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(JumpMovementFacade))]
    public class Movement : MonoBehaviour, IMovement
    {
        private JumpMovementFacade _movement;
        private Transform _enemyTransform;

        private Transform _currentTargetTransform;

        public float Direction => _movement.Direction;
        public float Speed => _movement.Speed;

        private void Awake()
        {
            _movement = GetComponent<JumpMovementFacade>();
        }

        public void Initialize(Transform enemyTransform)
        {
            _enemyTransform = enemyTransform;
        }

        private void FixedUpdate()
        {
            if (_currentTargetTransform != null)
                _movement.Move(_currentTargetTransform.position.x - _enemyTransform.position.x, Time.fixedDeltaTime);
            else
                _movement.Move(0, 0);
        }

        public void Follow(Transform target)
        {
            _currentTargetTransform = target;
        }

        public float GetDistanceByTarget()
        {
            if (_currentTargetTransform != null)
                return DirectionConst.GetDistance(_enemyTransform.position.x, _currentTargetTransform.position.x);

            return 0;
        }

        public void StopFollowing()
        {
            _currentTargetTransform = null;
        }
    }
}