using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private PatrollPath _patrollPath;

        [SerializeField] private float _maxAttackDistance;
        [SerializeField] private float _minAttackDistance;
        [SerializeField] private float _maxFollowDistance;

        private List<BaseState> _states = new List<BaseState>();
        private BaseState _currentState;

        public void Initialize(Character character, Movement movement, Detector detector)
        {
            _states.Add(new PatrolleState(_patrollPath, detector, this, movement));
            _states.Add(new FollowState(character.transform, _maxFollowDistance, _minAttackDistance, this, movement));
            _states.Add(new AttackState(_maxAttackDistance, character.transform, this, movement));

            ChangeState<PatrolleState>();
        }

        private void Update()
        {
            if (_currentState != null)
                _currentState.Update();
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

    public abstract class BaseState : IUpdateable
    {
        public BaseState(StateMachine stateMachine, Movement movement)
        {
            StateMachine = stateMachine;
            Movement = movement;
        }

        protected Movement Movement { get; private set; }
        protected StateMachine StateMachine { get; private set; }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }

    public class PatrolleState : BaseState
    {
        private readonly PatrollPath _patrollPath;
        private readonly Detector _detector;

        public PatrolleState(PatrollPath patrollPath, Detector detector, StateMachine stateMachine, Movement movement) : base(stateMachine, movement)
        {
            _patrollPath = patrollPath;
            _detector = detector;
        }

        public override void Enter()
        {
            Movement.Follow(_patrollPath.GetNextPathPoint());
            _detector.PlayerDetected += OnPlayerDetected;
        }

        public override void Exit()
        {
            Movement.StopFollowing();
            _detector.PlayerDetected -= OnPlayerDetected;
        }

        public override void Update()
        {
            if (Movement.GetDistanceByTarget() < 0.1)
                Movement.Follow(_patrollPath.GetNextPathPoint());
        }

        private void OnPlayerDetected()
        {
            StateMachine.ChangeState<FollowState>();
        }
    }

    public class FollowState : BaseState
    {
        private readonly Transform _player;

        private readonly float _maxFollowDistance;
        private readonly float _minAttackDistance;

        public FollowState(Transform player, float maxFollowDistance, float minAttackDistance, StateMachine stateMachine, Movement movement) : base(stateMachine, movement)
        {
            _player = player;

            _maxFollowDistance = maxFollowDistance;
            _minAttackDistance = minAttackDistance;
        }

        public override void Enter()
        {
            Movement.Follow(_player);
        }

        public override void Exit()
        {
            Movement.StopFollowing();
        }

        public override void Update()
        {
            float distanceByTarget = Movement.GetDistanceByTarget();

            if (distanceByTarget > _maxFollowDistance)
                StateMachine.ChangeState<PatrolleState>();
            else if (distanceByTarget < _minAttackDistance)
                StateMachine.ChangeState<AttackState>();
        }
    }

    public class AttackState : BaseState
    {
        private readonly Transform _player;
        private float _maxAttackDistance;

        public AttackState(float maxAttackDistance, Transform player, StateMachine stateMachine, Movement movement) : base(stateMachine, movement)
        {
            _player = player;
            _maxAttackDistance = maxAttackDistance;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            float distanceByTarget = DirectionConst.GetDistance(Movement.transform.position.x, _player.position.x);

            if (distanceByTarget > _maxAttackDistance)
                StateMachine.ChangeState<FollowState>();
            else
                Attack();
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }
    }
}