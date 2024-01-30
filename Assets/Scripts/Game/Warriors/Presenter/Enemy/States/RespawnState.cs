﻿namespace EnemyNamespace
{
    public class RespawnState : BaseState
    {
        private EnemyView _enemyView;

        public RespawnState(EnemyPresenter stateMachine, EnemyView enemyView) : base(stateMachine)
        {
            _enemyView = enemyView;
        }

        public override void Enter()
        {
            _enemyView.Respawn();
            _enemyView.Respawned += OnRespawnded;
        }

        public override void Exit()
        {
            _enemyView.Respawned -= OnRespawnded;
        }

        public override void Update(float timeBetweenFrame)
        {
        }

        private void OnRespawnded()
        {
            EnemyPresenter.ChangeState<PatrolleState>();
        }
    }
}