using System;

namespace CharacterNamespace
{
    public class CharacterPresenter : WarriarPresenter
    {
        private readonly CharacterData _data;
        private readonly Health _health;

        private CharacterModel _model;

        public CharacterPresenter
        (
            CharacterView view,
            CharacterData data,
            Health health,
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

            if (health == null)
                throw new ArgumentNullException();

            HealthChangeable = health;
            _health = health;
            _data = data;
        }

        public override IHealthChangeable HealthChangeable { get; protected set; }

        public void Heal(int value)
        {
            _model.Heal(value);
        }

        protected override WarriarModel CreateModel()
        {
            _model = new CharacterModel(_health);

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