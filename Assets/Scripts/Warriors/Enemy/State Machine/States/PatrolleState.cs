namespace Enemy
{
    public class PatrolleState : BaseState
    {
        public PatrolleState(StateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            StateMachine.Enemy.Movement.Follow(StateMachine.Enemy.PatrollPath.GetNextPathPoint());
            StateMachine.Enemy.Detector.Detected += OnPlayerDetected;
        }

        public override void Exit()
        {
            StateMachine.Enemy.Movement.StopFollowing();
            StateMachine.Enemy.Detector.Detected -= OnPlayerDetected;
        }

        public override void Update(float timeBetweenFrame)
        {
            if (StateMachine.Enemy.Movement.GetDistanceByTarget() < 0.1)
                StateMachine.Enemy.Movement.Follow(StateMachine.Enemy.PatrollPath.GetNextPathPoint());
        }

        private void OnPlayerDetected(Health health)
        {
            StateMachine.ChangeState<FollowState>();
        }
    }
}