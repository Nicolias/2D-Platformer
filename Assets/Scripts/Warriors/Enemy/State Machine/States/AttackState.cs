using CharacterSystem;

namespace Enemy
{
    public class AttackState : BaseState
    {
        private readonly Character _player;

        public AttackState(Character player, StateMachine stateMachine) : base(stateMachine)
        {
            _player = player;
        }

        public override void Enter()
        {
            StateMachine.Enemy.AttackAndHealth.Attacker.StartAttack(_player.AttackAndHealth.Health);
            _player.Died += OnPlayerDead;
        }

        public override void Exit()
        {
            StateMachine.Enemy.AttackAndHealth.Attacker.StopAttack();
            _player.Died -= OnPlayerDead;
        }

        public override void Update(float timeBetweenFrame)
        {
            if (StateMachine.Enemy.CanAttack(_player.transform.position) == false)
                StateMachine.ChangeState<FollowState>();
        }

        private void OnPlayerDead()
        {
           StateMachine.ChangeState<RespawnState>();
        }
    }
}