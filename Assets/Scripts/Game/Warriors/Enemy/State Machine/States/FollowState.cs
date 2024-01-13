using UnityEngine;

namespace Enemy
{
    public class FollowState : BaseState
    {
        private readonly Transform _player;

        private readonly float _maxFollowDistance;

        public FollowState(Transform player, float maxFollowDistance, StateMachine stateMachine) : base(stateMachine)
        {
            _player = player;

            _maxFollowDistance = maxFollowDistance;
        }

        public override void Enter()
        {
            StateMachine.Enemy.Movement.Follow(_player);
        }

        public override void Exit()
        {
            StateMachine.Enemy.Movement.StopFollowing();
        }

        public override void Update(float timeBetweenFrame)
        {
            float distanceByTarget = StateMachine.Enemy.Movement.GetDistanceByTarget();

            if (distanceByTarget > _maxFollowDistance)
                StateMachine.ChangeState<RespawnState>();
            else if (StateMachine.Enemy.CanAttack(_player.position))
                StateMachine.ChangeState<AttackState>();
        }
    }
}