namespace Enemy
{
    public class PatrolleState : BaseState
    {
        private readonly PatrollPath _patrollPath;
        private readonly Detector _detector;

        public PatrolleState(PatrollPath patrollPath, Detector detector, StateMachine stateMachine, Movement movement) : base(stateMachine, movement)
        {
            _patrollPath = patrollPath;
            _detector = detector;
        }

        public override void Enter()
        {
            Movement.Follow(_patrollPath.GetNextPathPoint());
            _detector.PlayerDetected += OnPlayerDetected;
        }

        public override void Exit()
        {
            Movement.StopFollowing();
            _detector.PlayerDetected -= OnPlayerDetected;
        }

        public override void Update(float timeBetweenFrame)
        {
            if (Movement.GetDistanceByTarget() < 0.1)
                Movement.Follow(_patrollPath.GetNextPathPoint());
        }

        private void OnPlayerDetected()
        {
            StateMachine.ChangeState<FollowState>();
        }
    }
}