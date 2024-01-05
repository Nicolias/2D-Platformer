using CharacterSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class StateMachine : MonoBehaviour, IUpdateable, IDisposable
    {
        [SerializeField] private float _maxFollowDistance;
        
        private UpdateServise _updateServise;

        private List<BaseState> _states = new List<BaseState>();
        private BaseState _currentState;

        public Enemy Enemy { get; private set; }

        public void Initialize(Enemy enemy, Character character, UpdateServise updateServise)
        {
            _updateServise = updateServise;
            Enemy = enemy;

            _states.Add(new RespawnState(this));
            _states.Add(new PatrolleState(this));
            _states.Add(new FollowState(character.transform, _maxFollowDistance, this));
            _states.Add(new AttackState(character, this));

            updateServise.AddToUpdate(this);
        }

        public void Dispose()
        {
            _updateServise.RemoveFromUpdate(this);
        }

        void IUpdateable.Update(float timeBetweenFrame)
        {
            if (_currentState != null)
                _currentState.Update(timeBetweenFrame);
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
    }
}