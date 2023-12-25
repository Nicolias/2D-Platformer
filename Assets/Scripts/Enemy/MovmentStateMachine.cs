using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(FollowingMovement))]
    [RequireComponent(typeof(PatrollingMovement))]
    public class MovmentStateMachine : MonoBehaviour
    {
        private FollowingMovement _followingMovement;
        private PatrollingMovement _patrollingMovement;

        private List<BaseState> _states = new List<BaseState>();
        private BaseState _currentState;

        public IMovement CurrentMovement => _currentState.Movement;

        public event UnityAction MovementChanged;

        public void Initialize(Character character)
        {
            _followingMovement = GetComponent<FollowingMovement>();
            _patrollingMovement = GetComponent<PatrollingMovement>();

            _states.Add(new PatrollingState(_patrollingMovement));
            _states.Add(new FollowState(_followingMovement, character.transform));

            ChangeState<PatrollingState>();
        }

        public void ChangeState<T>() where T : BaseState
        {
            if (_currentState is T)
                return;

            if (_currentState != null)
                _currentState.Exit();

            _currentState = _states.FirstOrDefault(s => s is T);
            _currentState.Enter();

            MovementChanged?.Invoke();
        }
    }

    public abstract class BaseState
    {
        protected BaseState(IMovement movement)
        {
            Movement = movement;
        }

        public IMovement Movement { get; protected set; }

        public abstract void Enter();

        public abstract void Exit();
    }

    public class PatrollingState : BaseState
    {
        private readonly PatrollingMovement _patrollingMovement;

        public PatrollingState(PatrollingMovement patrollingMovement) : base(patrollingMovement)
        {
            _patrollingMovement = patrollingMovement;
        }

        public override void Enter()
        {
            _patrollingMovement.enabled = true;
        }

        public override void Exit()
        {
            _patrollingMovement.enabled = false;
        }
    }

    public class FollowState : BaseState
    {
        private readonly FollowingMovement _followingMovement;
        private readonly Transform _player;

        public FollowState(FollowingMovement followingMovement, Transform player) : base(followingMovement)
        {
            _followingMovement = followingMovement;
            _player = player;
        }

        public override void Enter()
        {
            _followingMovement.Follow(_player);
            _followingMovement.enabled = true;
        }

        public override void Exit()
        {
            _followingMovement.enabled = false;
        }
    }
}