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

            _health.Changed += OnHealthChanged;

            OnInitialized();
            OnHealthChanged(MaxHealth);
        }

        private void OnDestroy()
        {
            _health.Changed -= OnHealthChanged;
        }

        protected virtual void OnInitialized() { }

        protected abstract void OnHealthChanged(float currentHealth);
    }
}
