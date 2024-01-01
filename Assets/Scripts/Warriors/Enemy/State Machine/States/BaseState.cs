namespace Enemy
{
    public abstract class BaseState : IUpdateable
    {
        public BaseState(StateMachine stateMachine, Movement movement)
        {
            StateMachine = stateMachine;
            Movement = movement;
        }

        protected Movement Movement { get; private set; }
        protected StateMachine StateMachine { get; private set; }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update(float timeBetweenFrame);
    }
}