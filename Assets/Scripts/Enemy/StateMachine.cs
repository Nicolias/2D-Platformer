using CharacterSystem;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace Enemy
{    public class StateMachine : MonoBehaviour, IUpdateable
    {
        [SerializeField] private float _maxAttackDistance;
        [SerializeField] private float _minAttackDistance;
        [SerializeField] private float _maxFollowDistance;
        
        private List<BaseState> _states = new List<BaseState>();
        private BaseState _currentState;

        public void Initialize(Enemy enemy, Character character, UpdateServise updateServise)
        {
            updateServise.AddToUpdate(this);
            _states.Add(new RespawnState(this, enemy.Movement, enemy));
            _states.Add(new PatrolleState(enemy.PatrollPath, enemy.Detector, this, enemy.Movement));
            _states.Add(new FollowState(character.transform, _maxFollowDistance, _minAttackDistance, this, enemy.Movement));
            _states.Add(new AttackState(_maxAttackDistance, character, enemy, this, enemy.Movement));

            ChangeState<RespawnState>();
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

        public abstract void Update(float timeBetweenFrame);
    }

    public class RespawnState : BaseState
    {
        private readonly Enemy _enemy;

        public RespawnState(StateMachine stateMachine, Movement movement, Enemy enemy) : base(stateMachine, movement)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            _enemy.TeleportToStartPoint();
        }

        public override void Exit()
        {
        }

        public override void Update(float timeBetweenFrame)
        {
        }
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

        public override void Update(float timeBetweenFrame)
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

        public override void Update(float timeBetweenFrame)
        {
            float distanceByTarget = Movement.GetDistanceByTarget();

            if (distanceByTarget > _maxFollowDistance)
                StateMachine.ChangeState<RespawnState>();
            else if (distanceByTarget < _minAttackDistance)
                StateMachine.ChangeState<AttackState>();
        }
    }

    public class AttackState : BaseState
    {
        private readonly Character _player;
        private readonly Enemy _enemy;
        private float _maxAttackDistance;

        public AttackState(float maxAttackDistance, Character player, Enemy enemy, StateMachine stateMachine, Movement movement) : base(stateMachine, movement)
        {
            _player = player;
            _maxAttackDistance = maxAttackDistance;
            _enemy = enemy;
        }

        public override void Enter()
        {
            _enemy.Attacker.StartAttack(_player.Health);
        }

        public override void Exit()
        {
            _enemy.Attacker.StopAttack();
        }

        public override void Update(float timeBetweenFrame)
        {
            float distanceByTarget = DirectionConst.GetDistance(_enemy.transform.position.x, _player.transform.position.x);

            if (distanceByTarget > _maxAttackDistance)
                StateMachine.ChangeState<FollowState>();
        }
    }
}