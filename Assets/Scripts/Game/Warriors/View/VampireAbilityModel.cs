using System;
using System.Threading.Tasks;

namespace CharacterNamespace
{
    public class VampireAbilityModel
    {
        private readonly Health _characterHealth;

        private readonly int _drainDuration;
        private readonly int _drainValuePerSeconds;

        private bool _isDreining = false;
        private IDamagable _target;

        public event Action DrainEnded;

        public VampireAbilityModel(Health characterHealth, int drainDuration, int drainValuePerSeconds)
        {
            _characterHealth = characterHealth;
            _drainDuration = drainDuration;
            _drainValuePerSeconds = drainValuePerSeconds;
        }

        public void Drain(IDamagable target)
        {
            _target = target;

            if (_isDreining == false)
                DrainLife();
        }

        private async Task DrainLife()
        {
            int milisecondsInSeconds = 1000;
            _isDreining = true;

            for (int i = 0; i < _drainDuration; i++)
            {
                if (_target != null)
                {
                    _target.Damage(_drainValuePerSeconds);
                    _characterHealth.Heal(_drainValuePerSeconds);
                }

                await Task.Delay(milisecondsInSeconds);
            }

            _isDreining = false;
            DrainEnded?.Invoke();
        }
    }
}
