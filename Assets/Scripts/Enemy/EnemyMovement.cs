using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
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
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        int left = -1;
        int right = 1;

        while (enabled)
        {
            Transform currentPathPoint = _pathPoints.Dequeue();

            while (_transform.position.x != currentPathPoint.position.x)
            {
                if (_transform.position.x > currentPathPoint.position.x)
                    Direction = left;
                else if (_transform.position.x < currentPathPoint.position.x)
                    Direction = right;

                _transform.position = new Vector2(Mathf.MoveTowards(_transform.position.x, currentPathPoint.position.x, _speed * Time.deltaTime), 0);
                yield return null;
            }

            _pathPoints.Enqueue(currentPathPoint);

            yield return null;
        }
    }
}
