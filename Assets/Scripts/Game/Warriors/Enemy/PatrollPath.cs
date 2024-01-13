using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class PatrollPath : MonoBehaviour
    {
        [SerializeField] private List<Transform> _patrollPathList;

        private Queue<Transform> _pathPoints = new Queue<Transform>();

        private void Awake()
        {
            Reset();
        }

        public void Reset()
        {
            _pathPoints.Clear();
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