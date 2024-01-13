namespace Enemy
{
    public class RespawnState : BaseState
    {

        public RespawnState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Enemy.MoveAnimation.Animator.SetTrigger(AnimatorData.Params.Teleport);
        }

        public override void Exit()
        {
        }

        public override void Update(float timeBetweenFrame)
        {
        }
    }
}