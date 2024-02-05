using UnityEngine;

namespace HealthView
{
    public abstract class AbstractHealthView : MonoBehaviour
    {
        private IHealthChangeable _health;

        protected float MaxHealth => _health.MaxValue;

        public void Initialize(IHealthChangeable health)
        {
            _health = health;


            OnInitialized();
            OnHealthChanged(MaxHealth);
        }

        public void Enable()
        {
           _health.Changed += OnHealthChanged;
        }

        public void Disable()
        {
            _health.Changed -= OnHealthChanged;
        }

        protected virtual void OnInitialized() { }

        protected abstract void OnHealthChanged(float currentHealth);
    }
}
