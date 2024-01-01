using UnityEngine;

namespace Enemy
{
    public class Movement : IMovement, IUpdateable
    {
        private readonly JumpMovementFacade _movement;
        private readonly Transform _enemyTransform;

        private Transform _currentTargetTransform;

        public Movement(Transform enemyTransform, JumpMovementFacade jumpMovementFacade, UpdateServise updateServise)
        {
            _enemyTransform = enemyTransform;
            _movement = jumpMovementFacade;

            updateServise.AddToFixedUpdate(this);
        }

        public float Direction => _movement.Direction;
        public float Speed => _movement.Speed;

        void IUpdateable.Update(float timeBetweenFrame)
        {
            if (_currentTargetTransform != null)
                _movement.Move(_currentTargetTransform.position.x - _enemyTransform.position.x, timeBetweenFrame);
            else
                _movement.Move(0, 0);
        }

        public void Respawn(Vector2 respawnPoint)
        {
            _movement.Teleport(respawnPoint);
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