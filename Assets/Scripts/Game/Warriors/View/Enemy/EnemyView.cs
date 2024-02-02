using CharacterNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNamespace
{
    [RequireComponent(typeof(Movement))]
    public class EnemyView : WarriarView, IDieable
    {
        [SerializeField] private Detector _detector;
        [SerializeField] private List<Transform> _patrollPathList;

        private CharacterView _target;
        private EnemyPresenter _behaviourPresenter;

        protected override AbstractDetector Detector => _detector;

        [field: SerializeField] public EnemyData Data { get; private set; }

        public Input Input { get; private set; }

        public event Action Respawned;

        public void Initialize(UpdateServise updateServise, CharacterView target)
        {
            if (updateServise == null) throw new ArgumentNullException();

            if (target == null) throw new ArgumentNullException();

            _target = target;

            Initialize(updateServise);
        }

        protected override void OnEnabled()
        {
            AnimationEventsHandler.TeleportShowed += OnRespawned;
            UpdateServise.AddToFixedUpdate(Input);
        }

        protected override void OnDisabled()
        {
            AnimationEventsHandler.TeleportShowed -= OnRespawned;
            UpdateServise.RemoveFromFixedUpdate(Input);
        }

        public void Respawn()
        {
            AnimationHandler.Animator.SetTrigger(AnimatorData.Params.Teleport);
        }

        protected override AbstractInput GetMoveInput(Movement movementController)
        {
            if (movementController == null) throw new ArgumentNullException();

            Input = new Input(transform, movementController, new PatrollPath(_patrollPathList));
            return Input;
        }

        protected override WarriarPresenter GetPresenter()
        {
            _behaviourPresenter = new EnemyPresenter
            (
                this,
                Data,
                UpdateServise,
                _target
            );

            return _behaviourPresenter;
        }

        protected override Movement GetMovementController(CharacterController characterController)
        {
            if (characterController == null) throw new ArgumentNullException();

            return Data.CreateMovementController(characterController);
        }

        private void OnRespawned()
        {
            Input.Respawn();
            Respawned?.Invoke();
        }
    }
}