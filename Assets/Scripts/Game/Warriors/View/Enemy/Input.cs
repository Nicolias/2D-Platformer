using System;
using UnityEngine;

namespace EnemyNamespace
{
    public class Input : AbstractInput
    {
        private readonly Movement _movement;
        private readonly PatrollPath _patrollPath;
        private readonly Transform _selfTransform;

        private Transform _currentTargetTransform;

        public Input(Transform transform, Movement moveController, PatrollPath patrollPath)
        {
            if (transform == null) throw new ArgumentNullException();
            if (moveController == null) throw new ArgumentNullException();
            if (patrollPath == null) throw new ArgumentNullException();

            _selfTransform = transform;
            _movement = moveController;
            _patrollPath = patrollPath;
        }

        public void Respawn()
        {
            _patrollPath.Reset();
            _movement.Teleport(_patrollPath.GetNextPathPoint().position);
        }

        public void Follow(Transform target)
        {
            if (target == null) throw new ArgumentNullException();

            _currentTargetTransform = target;
        }

        public void StopFollowing()
        {
            _currentTargetTransform = null;
        }

        public float GetDistanceByTarget()
        {
            if (_currentTargetTransform != null)
                return DirectionConst.GetDistance(_selfTransform.position.x, _currentTargetTransform.position.x);

            return 0;
        }

        public void GoToNextPotrollPath()
        {
            Follow(_patrollPath.GetNextPathPoint());
        }

        protected override void FixedUpdate(float timeBetweenFrame)
        {
            if (_currentTargetTransform != null)
                _movement.Move(_currentTargetTransform.position.x - _selfTransform.position.x, timeBetweenFrame);
            else
                _movement.Move(0, 0);
        }
    }
}