using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNamespace
{
    public class VampireAbilityPresenter : IUpdateable
    {
        private readonly VampireAbilityView _view;
        private readonly VampireAbilityModel _model;

        private bool _isEnabled;

        private List<IDamagable> _nearbyTargets = new List<IDamagable>();

        public VampireAbilityPresenter(VampireAbilityView view, VampireAbilityModel model)
        {
            if (view == null)
                throw new ArgumentNullException();

            if (model == null)
                throw new ArgumentNullException();

            _view = view;
            _model = model;
        }

        void IUpdateable.Update(float timeBetweenFrame)
        {
            if (_isEnabled == false)
                return;

            IDamagable neareasTarget = FindNearestTarget();

            _model.Drain(neareasTarget);
            _view.DrawLine(neareasTarget);
        }

        public void Enable()
        {
            _isEnabled = true;
            _model.DrainEnded += Disable;
        }

        public void Disable()
        {
            _model.DrainEnded -= Disable;
            _isEnabled = false;
            _view.DrawLine(null);
            _nearbyTargets.Clear();
        }

        public void Add(IDamagable target)
        {
            _nearbyTargets.Add(target);
        }

        public void Remove(IDamagable target)
        {
            _nearbyTargets.Remove(target);
        }

        private IDamagable FindNearestTarget()
        {
            IDamagable neareasTarget = null;
            float distanceByNeareasTarget = float.MaxValue;

            foreach (IDamagable target in _nearbyTargets)
            {
                float distanceByTarget = GetDistance(target.Position);

                if (neareasTarget == null || distanceByTarget < distanceByNeareasTarget)
                {
                    neareasTarget = target;
                    distanceByNeareasTarget = distanceByTarget;
                }
            }

            return neareasTarget;
        }

        private float GetDistance(Vector2 targetPosition)
        {
            float x = MathF.Abs(_view.Position.x - targetPosition.x);
            float y = MathF.Abs(_view.Position.y - targetPosition.y);

            return MathF.Sqrt(MathF.Pow(x, 2) + MathF.Pow(y, 2));
        }
    }
}
