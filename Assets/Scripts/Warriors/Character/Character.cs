using System;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterSystem
{
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour, IDieable
    {
        [SerializeField] private JumpMovementFacade _movement;
        [SerializeField] private Detector _detector;

        private MoveAnimation _moveAnimation;

        [field: SerializeField] public CoinCollector CoinCollector { get; private set; }
        [field: SerializeField] public AttackAndHealthFacade AttackAndHealth { get; private set; }

        public MoveAnimation MoveAnimation => _moveAnimation;

        public event UnityAction Died;

        public void Initialize(UpdateServise updateServise)
        {
            _detector.Detected += OnDetected;
            _detector.Lost += OnEnemyLost;

            new Input(_movement, updateServise);
            _movement.Initialize(updateServise);
            _moveAnimation = new MoveAnimation(_movement, GetComponent<Animator>(), transform, updateServise);

            AttackAndHealth.Initialize(updateServise, this);
        }

        private void OnDisable()
        {
            _detector.Detected -= OnDetected;
            _detector.Lost -= OnEnemyLost;
        }

        void IDieable.Die()
        {
            _movement.Dispose();

            Died?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnDetected(Health health)
        {
            AttackAndHealth.Attacker.StartAttack(health);
        }

        private void OnEnemyLost()
        {
            AttackAndHealth.Attacker.StopAttack();
        }
    }
}