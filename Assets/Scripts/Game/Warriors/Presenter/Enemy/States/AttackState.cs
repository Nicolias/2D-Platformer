using CharacterNamespace;

namespace EnemyNamespace
{
    public class AttackState : BaseState
    {
        private readonly WarriarView _player;

        public AttackState(CharacterView player, EnemyPresenter stateMachine) : base(stateMachine)
        {
            _player = player;
        }

        public override void Enter()
        {
            EnemyPresenter.StartAttack(_player);
            _player.Died += OnPlayerDead;
        }

        public override void Exit()
        {
            EnemyPresenter.StopAttack();
            _player.Died -= OnPlayerDead;
        }

        public override void Update(float timeBetweenFrame)
        {
            if (EnemyPresenter.CanAttack(_player.transform.position) == false)
                EnemyPresenter.ChangeState<FollowState>();
        }

        private void OnPlayerDead()
        {
           EnemyPresenter.ChangeState<RespawnState>();
        }
    }
}