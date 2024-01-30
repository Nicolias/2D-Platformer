namespace EnemyNamespace
{
    public class PatrolleState : BaseState
    {
        private readonly Input _input;

        public PatrolleState(EnemyPresenter stateMachine, Input input) : base(stateMachine)
        {
            _input = input;
        }

        public override void Enter()
        {
            _input.GoToNextPotrollPath();
        }

        public override void Exit()
        {
            _input.StopFollowing();
        }

        public override void Update(float timeBetweenFrame)
        {
            if (_input.GetDistanceByTarget() < 0.1)
                _input.GoToNextPotrollPath();
        }

        public override void PlayerDetected(IDamagable damagable)
        {
            EnemyPresenter.ChangeState<FollowState>();
        }
    }
}