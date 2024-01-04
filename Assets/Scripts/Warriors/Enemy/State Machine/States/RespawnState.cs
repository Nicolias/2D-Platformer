namespace Enemy
{
    public class RespawnState : BaseState
    {

        public RespawnState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Enemy.TeleportToStartPoint();
        }

        public override void Exit()
        {
        }

        public override void Update(float timeBetweenFrame)
        {
        }
    }
}