using UnityEngine;

namespace Enemy
{
    public class FollowState : BaseState
    {
        private readonly Transform _player;

        private readonly float _maxFollowDistance;
        private readonly float _attackDistance;

        public FollowState(Transform player, float maxFollowDistance, float attackDistance, StateMachine stateMachine, Movement movement) : base(stateMachine, movement)
        {
            _player = player;

            _maxFollowDistance = maxFollowDistance;
            _attackDistance = attackDistance;
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
            else if (distanceByTarget <= _attackDistance)
                StateMachine.ChangeState<AttackState>();
        }
    }
}