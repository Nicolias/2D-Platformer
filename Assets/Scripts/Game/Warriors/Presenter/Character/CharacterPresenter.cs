using System;

namespace CharacterNamespace
{
    public class CharacterPresenter : WarriarPresenter
    {
        private readonly CharacterData _data;

        private CharacterModel _model;

        public CharacterPresenter
        (
            CharacterView view,
            CharacterData data,
            UpdateServise updateServise
        ) :
        base
        (
            view,
            data.CreateAttacker(updateServise)
        )
        {
            if (data == null)
                throw new ArgumentNullException();

            _data = data;
        }

        public override IHealthChangeable HealthChangeable { get; protected set; }

        public void Heal(int value)
        {
            _model.Heal(value);
        }

        protected override WarriarModel CreateModel()
        {
            Health health = _data.CreateHealth();

            _model = new CharacterModel(health);
            HealthChangeable = health;

            return _model;
        }

        protected override void OnTargetDetected(IDamagable damagable)
        {
            if (damagable == null) throw new ArgumentNullException();

            Attacker.StartAttack(damagable);
        }

        protected override void OnTargetLost()
        {
            Attacker.StopAttack();
        }
    }
}