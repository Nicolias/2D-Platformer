using System;
using UnityEngine;

namespace CharacterNamespace
{
    public class CharacterView : WarriarView, IHealabel
    {
        [SerializeField] private Detector _detector;
        [SerializeField] private VampireAbilityView _vampireView;
        private Health _health;
        private CharacterPresenter _characterPresenter;
        private CharacterController _characterController;

        protected override AbstractDetector Detector => _detector;

        [field: SerializeField] public CharacterData Data { get; private set; }

        public void Heal(int value)
        {
            _characterPresenter.Heal(value);
        }

        protected override void OnInitialized()
        {
            _vampireView.Initialize(UpdateServise, _health);
        }

        protected override void OnEnabled()
        {
            Died += OnDied;
            UpdateServise.AddToUpdate(AbstractInput);
            _vampireView.Enable();
        }

        protected override void OnDisabled()
        {
            Died -= OnDied;
            UpdateServise.RemoveFromUpdate(AbstractInput);
            _vampireView.Disable();
        }

        protected override AbstractInput GetMoveInput(Movement movementController)
        {
            if(movementController == null) throw new ArgumentNullException();

            return new Input(movementController);
        }

        protected override WarriarPresenter GetPresenter()
        {
            _health = Data.CreateHealth();
            _characterPresenter = new CharacterPresenter(this, Data, _health, UpdateServise);

            return _characterPresenter;
        }

        protected override Movement GetMovementController(CharacterController characterController)
        {
            if (characterController == null) throw new ArgumentNullException();

            _characterController = characterController;

            return Data.CreateMovementController(characterController);
        }

        private void OnDied()
        {
            _characterController.enabled = false;
        }
    }
}