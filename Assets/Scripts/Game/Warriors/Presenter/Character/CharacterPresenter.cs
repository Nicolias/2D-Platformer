using System;

namespace CharacterNamespace
{
    public class CharacterPresenter : WarriarPresenter
    {
        private CharacterModel _model;

        public CharacterPresenter
        (
            CharacterView view,
            CharacterModel characterModel,
            WarriarData warriarData,
            UpdateServise updateServise
        ) :
        base
        (
            view,
            characterModel,
            warriarData.CreateAttacker(updateServise)
        )
        {
            if (characterModel == null) throw new ArgumentNullException();

            _model = characterModel;
        }

        public void Heal(int value)
        {
            _model.Heal(value);
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