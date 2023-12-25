using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class FollowingMovement : AbstractPhisicsMovement
    {
        [SerializeField] private float _maxFollowDistacne;
        [SerializeField] private float _minDistanceForJump;

        private Transform _target;

        protected override event UnityAction Jumping;

        public void Follow(Transform target)
        {
            _target = target;
        }

        protected override float GetHorizontalDirection()
        {
            Debug.Log(GetDistance(_target.position.x, transform.position.x));

            float distance = GetDistance(_target.position.x, transform.position.x);

            if (distance > _maxFollowDistacne)
                return Mathf.Clamp(_target.position.x - transform.position.x, -1, 1);
            else
                return 0;
        }

        protected override void Updating()
        {
            //if (_target != null)
            //{
            //    if (GetDistance(_target.position.y, transform.position.y) > _minDistanceForJump)
            //        Jumping?.Invoke();
            //}
        }

        private float GetDistance(float firstPoint, float secondPoint)
        {
            float maxValue = Mathf.Max(firstPoint, secondPoint);
            float minValue = Mathf.Min(firstPoint, secondPoint);

            return maxValue - minValue;
        }
    }
}