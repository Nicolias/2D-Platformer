namespace Enemy
{
    public class RespawnState : BaseState
    {
        private readonly Enemy _enemy;

        public RespawnState(StateMachine stateMachine, Movement movement, Enemy enemy) : base(stateMachine, movement)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            _enemy.TeleportToStartPoint();
        }

        public override void Exit()
        {
        }

        public override void Update(float timeBetweenFrame)
        {
        }
    }
}