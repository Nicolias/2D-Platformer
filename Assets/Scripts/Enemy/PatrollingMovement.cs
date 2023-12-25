using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class PatrollingMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private List<Transform> _pathPointsList;
        [SerializeField] private float _speed;

        private Queue<Transform> _pathPoints;

        private Transform _transform;

        public float Direction { get; private set; }
        public float Speed => _speed;

        private void Awake()
        {
            _pathPoints = new Queue<Transform>(_pathPointsList);

            _transform = transform;
        }

        private void OnEnable()
        {
            StartCoroutine(Move());
        }

        private void OnDisable()
        {
            StopCoroutine(Move());
        }

        private IEnumerator Move()
        {
            while (enabled)
            {
                Transform currentPathPoint = _pathPoints.Dequeue();

                while (enabled && _transform.position.x != currentPathPoint.position.x)
                {
                    if (_transform.position.x > currentPathPoint.position.x)
                        Direction = -1;
                    else if (_transform.position.x < currentPathPoint.position.x)
                        Direction = 1;

                    _transform.position = new Vector2(Mathf.MoveTowards(_transform.position.x, currentPathPoint.position.x, _speed * Time.deltaTime), 0);
                    yield return null;
                }

                _pathPoints.Enqueue(currentPathPoint);

                yield return null;
            }
        }
    }
}