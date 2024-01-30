using CharacterNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNamespace
{
    [RequireComponent(typeof(MovementController))]
    public class EnemyView : WarriarView, IDieable
    {
        [SerializeField] private Detector _detector;
        [SerializeField] private List<Transform> _patrollPathList;

        private CharacterView _target;
        private EnemyPresenter _behaviourPresenter;
        private Transform _selfTransform;

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

        protected override void OnInitialized()
        {
            _selfTransform = transform;
        }

        protected override void OnEnabled()
        {
            AnimationEventsHandler.TeleportShowed += OnRespawned;
        }

        protected override void OnDisabled()
        {
            AnimationEventsHandler.TeleportShowed -= OnRespawned;
        }

        public void Respawn()
        {
            AnimationHandler.Animator.SetTrigger(AnimatorData.Params.Teleport);
        }

        private void OnRespawned()
        {
            Input.Respawn();
            Respawned?.Invoke();
        }

        protected override AbstractInput GetMoveInput(MovementController movementController)
        {
            if (movementController == null) throw new ArgumentNullException();

            Input = new Input(_selfTransform, movementController, new PatrollPath(_patrollPathList));
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

        protected override MovementController GetMovementController(CharacterController characterController)
        {
            if (characterController == null) throw new ArgumentNullException();

            return Data.CreateMovementController(characterController);
        }
    }
}