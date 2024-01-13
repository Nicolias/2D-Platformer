namespace Enemy
{
    public abstract class BaseState : IUpdateable
    {
        public BaseState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected StateMachine StateMachine { get; private set; }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update(float timeBetweenFrame);
    }
}