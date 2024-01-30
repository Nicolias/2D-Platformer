using System;

namespace CharacterNamespace
{
    public class CharacterModel : WarriarModel
    {
        public CharacterModel(Health health) :
            base(health)
        {
        }

        public void Heal(int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException();

            Health.Heal(value);
        }
    }
}