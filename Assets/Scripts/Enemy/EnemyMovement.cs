using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _pathPointsList;
    [SerializeField] private float _moveTime;

    private Queue<Transform> _pathPoints;

    private Transform _transform;

    private void Awake()
    {
        _pathPoints = new Queue<Transform>(_pathPointsList);

        _transform = transform;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            Transform currentPathPoint = _pathPoints.Dequeue();

            while (_transform.position.x != currentPathPoint.position.x)
            {
                _transform.position = new Vector2(Mathf.MoveTowards(_transform.position.x, currentPathPoint.position.x, _moveTime * Time.deltaTime), 0);
                yield return null;
            }

            _pathPoints.Enqueue(currentPathPoint);

            yield return null;
        }
    }
}
