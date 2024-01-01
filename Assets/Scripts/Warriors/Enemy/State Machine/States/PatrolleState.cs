namespace Enemy
{
    public class PatrolleState : BaseState
    {
        private readonly PatrollPath _patrollPath;
        private readonly AbstractDetector _detector;

        public PatrolleState(PatrollPath patrollPath, AbstractDetector detector, StateMachine stateMachine, Movement movement) : base(stateMachine, movement)
        {
            _patrollPath = patrollPath;
            _detector = detector;
        }

        public override void Enter()
        {
            Movement.Follow(_patrollPath.GetNextPathPoint());
            _detector.Detected += OnPlayerDetected;
        }

        public override void Exit()
        {
            Movement.StopFollowing();
            _detector.Detected -= OnPlayerDetected;
        }

        public override void Update(float timeBetweenFrame)
        {
            if (Movement.GetDistanceByTarget() < 0.1)
                Movement.Follow(_patrollPath.GetNextPathPoint());
        }

        private void OnPlayerDetected(Health health)
        {
            StateMachine.ChangeState<FollowState>();
        }
    }
}