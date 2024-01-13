using Character;

namespace Enemy
{
    public class AttackState : BaseState
    {
        private readonly CharacterView _player;

        public AttackState(CharacterView player, StateMachine stateMachine) : base(stateMachine)
        {
            _player = player;
        }

        public override void Enter()
        {
            StateMachine.Enemy.AttackAndHealth.Attacker.StartAttack(_player.Model.Health);
            //_player.Died += OnPlayerDead;
        }

        public override void Exit()
        {
            StateMachine.Enemy.AttackAndHealth.Attacker.StopAttack();
            //_player.Died -= OnPlayerDead;
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