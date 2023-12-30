using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class PatrollPath : MonoBehaviour
    {
        [SerializeField] private List<Transform> _patrollPathList;

        private Queue<Transform> _pathPoints;

        private void Awake()
        {
            _pathPoints = new Queue<Transform>(_patrollPathList);
        }

        public Transform GetNextPathPoint()
        {
            Transform currentPathPoint = _pathPoints.Dequeue();
            _pathPoints.Enqueue(currentPathPoint);
            return currentPathPoint;
        }
    }
}