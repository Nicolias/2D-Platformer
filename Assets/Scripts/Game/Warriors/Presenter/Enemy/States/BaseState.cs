namespace EnemyNamespace
{
    public abstract class BaseState : IUpdateable
    {
        public BaseState(EnemyPresenter stateMachine)
        {
            EnemyPresenter = stateMachine;
        }

        protected EnemyPresenter EnemyPresenter { get; private set; }

        public abstract void Enter();

        public abstract void Exit();

        public virtual void Update(float timeBetweenFrame) { }

        public virtual void PlayerDetected(IDamagable damagable) { }

        public virtual void PlayerLost() { }
    }
}