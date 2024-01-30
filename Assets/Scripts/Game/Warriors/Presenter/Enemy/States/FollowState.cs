using UnityEngine;

namespace EnemyNamespace
{
    public class FollowState : BaseState
    {
        private readonly Transform _player;
        private readonly Input _input;

        private readonly float _maxFollowDistance;

        public FollowState(Transform player, Input input, float maxFollowDistance, EnemyPresenter enemyPresenter) : base(enemyPresenter)
        {
            _player = player;
            _input = input;

            _maxFollowDistance = maxFollowDistance;
        }

        public override void Enter()
        {
            _input.Follow(_player);
        }

        public override void Exit()
        {
            _input.StopFollowing();
        }

        public override void Update(float timeBetweenFrame)
        {
            float distanceByTarget = _input.GetDistanceByTarget();

            if (distanceByTarget > _maxFollowDistance)
                EnemyPresenter.ChangeState<RespawnState>();
            else if (EnemyPresenter.CanAttack(_player.position))
                EnemyPresenter.ChangeState<AttackState>();
        }
    }
}