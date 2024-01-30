using System;
using UnityEngine;

namespace CharacterNamespace
{
    public class CharacterView : WarriarView, IHealabel
    {
        [SerializeField] private Detector _detector;

        private CharacterPresenter _characterPresenter;

        protected override AbstractDetector Detector => _detector;

        [field: SerializeField] public CharacterData Data { get; private set; }

        public event Action<int> Healing;

        public void Heal(int value)
        {
            _characterPresenter.Heal(value);
        }

        protected override AbstractInput GetMoveInput(MovementController movementController)
        {
            if(movementController == null) throw new ArgumentNullException();

            return new Input(movementController);
        }

        protected override WarriarPresenter GetPresenter()
        {
            _characterPresenter = new CharacterPresenter(this, new CharacterModel(Data.CreateHealth()), Data, UpdateServise);

            return _characterPresenter;
        }

        protected override MovementController GetMovementController(CharacterController characterController)
        {
            if (characterController == null) throw new ArgumentNullException();

            return Data.CreateMovementController(characterController);
        }
    }
}