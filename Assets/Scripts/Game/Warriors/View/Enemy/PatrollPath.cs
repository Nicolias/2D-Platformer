using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNamespace
{
    public class PatrollPath
    {
        private readonly IEnumerable<Transform> _patrollPathList;

        private Queue<Transform> _pathPoints = new Queue<Transform>();

        public PatrollPath(IEnumerable<Transform> patrollPath)
        {
            if (patrollPath == null) throw new ArgumentNullException();

            if (patrollPath.Any(patrollPoint => patrollPoint == null)) throw new NullReferenceException();

            _patrollPathList = patrollPath;
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