using CharacterSystem;

namespace Enemy
{
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
            _enemy.AttackAndHealth.Attacker.StartAttack(_player.AttackAndHealth.Health);
            _player.Died += OnPlayerDead;
        }


        public override void Exit()
        {
            _enemy.AttackAndHealth.Attacker.StopAttack();
            _player.Died -= OnPlayerDead;
        }

        public override void Update(float timeBetweenFrame)
        {
            float distanceByTarget = DirectionConst.GetDistance(_enemy.transform.position.x, _player.transform.position.x);

            if (distanceByTarget > _maxAttackDistance)
                StateMachine.ChangeState<FollowState>();
        }

        private void OnPlayerDead()
        {
           StateMachine.ChangeState<RespawnState>();
        }
    }
}