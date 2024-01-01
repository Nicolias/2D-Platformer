using CharacterSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class StateMachine : MonoBehaviour, IUpdateable
    {
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _maxFollowDistance;
        
        private List<BaseState> _states = new List<BaseState>();
        private BaseState _currentState;

        public void Initialize(Enemy enemy, Character character, UpdateServise updateServise)
        {
            updateServise.AddToUpdate(this);
            _states.Add(new RespawnState(this, enemy.Movement, enemy));
            _states.Add(new PatrolleState(enemy.PatrollPath, enemy.Detector, this, enemy.Movement));
            _states.Add(new FollowState(character.transform, _maxFollowDistance, _attackDistance, this, enemy.Movement));
            _states.Add(new AttackState(_attackDistance, character, enemy, this, enemy.Movement));

            enemy.TeleportToStartPoint(false);
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