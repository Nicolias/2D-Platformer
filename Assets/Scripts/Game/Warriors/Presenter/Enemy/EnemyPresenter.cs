using CharacterNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemyNamespace
{
    public class EnemyPresenter : WarriarPresenter, IUpdateable
    {
        private readonly List<BaseState> _states = new List<BaseState>();

        private readonly EnemyView _view;
        private readonly EnemyConfig _config;
        private readonly UpdateServise _updateServise;

        private BaseState _currentState;

        public EnemyPresenter(EnemyView view, EnemyData data, UpdateServise updateServise, CharacterView target) : 
            base(view, new EnemyModel(data.CreateHealth()), data.CreateAttacker(updateServise))
        {
            if (view == null) throw new ArgumentNullException();
            if (data == null) throw new ArgumentNullException();
            if (updateServise == null) throw new ArgumentNullException();
            if (target == null) throw new ArgumentNullException();

            _view = view;
            _config = data.CreateConfig();
            _updateServise = updateServise;

            _states.Add(new RespawnState(this, view));
            _states.Add(new PatrolleState(this, view.Input));
            _states.Add(new FollowState(target.transform, view.Input, _config.MaxFollowDistance, this));
            _states.Add(new AttackState(target, this));

            ChangeState<RespawnState>();
        }

        void IUpdateable.Update(float timeBetweenFrame)
        {
            if (_currentState != null)
                _currentState.Update(timeBetweenFrame);
        }

        public void StartAttack(IDamagable damagable)
        {
            Attacker.StartAttack(damagable);
        }

        public void StopAttack()
        {
            Attacker.StopAttack();
        }

        public void ChangeState<T>() where T : BaseState
        {
            if (_currentState is T)
                return;

            if (_currentState != null)
                _currentState.Exit();

            _currentState = _states.FirstOrDefault(s => s is T);
            _currentState.Enter();
        }

        public bool CanAttack(Vector2 targetPosition)
        {
            return DirectionConst.GetDistance(_view.transform.position.x, targetPosition.x) <= _config.MaxAttackDistanceByXAxis
                && DirectionConst.GetDistance(_view.transform.position.y, targetPosition.y) <= _config.MaxAttackDistanceByYAxis;
        }

        protected override void OnEnabled()
        {
            _updateServise.AddToUpdate(this);
        }

        protected override void OnDisabled()
        {
            _updateServise.RemoveFromUpdate(this);
        }

        protected override void OnTargetDetected(IDamagable damagable)
        {
            _currentState.PlayerDetected(damagable);
        }

        protected override void OnTargetLost()
        {
            _currentState.PlayerLost();
        }
    }
}