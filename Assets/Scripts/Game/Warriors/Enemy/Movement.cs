using System;
using UnityEngine;

namespace Enemy
{
    public class Movement : IMoveable, IFixedUpdateable, IDisposable
    {
        private readonly MovementController _movement;
        private readonly Transform _enemyTransform;

        private readonly UpdateServise _updateServise;

        private Transform _currentTargetTransform;

        public Movement(Transform enemyTransform, MovementController jumpMovementFacade, UpdateServise updateServise)
        {
            _enemyTransform = enemyTransform;
            _movement = jumpMovementFacade;

            _updateServise = updateServise;
            updateServise.AddToFixedUpdate(this);
        }

        public DirectionType Direction => _movement.Direction;
        public float Speed => _movement.Speed;

        public void Dispose()
        {
            _updateServise.RemoveFromFixedUpdate(this);
        }

        void IFixedUpdateable.Update(float timeBetweenFrame)
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