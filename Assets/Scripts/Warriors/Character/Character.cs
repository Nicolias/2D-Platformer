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

            new Input(_movement, updateServise);
            _movement.Initialize(updateServise);
            _moveAnimation = new MoveAnimation(_movement, GetComponent<Animator>(), transform, updateServise);

            AttackAndHealth.Initialize(updateServise, this);
        }

        void IDieable.Die()
        {
            _movement.Dispose();

            Died?.Invoke();
            Destroy(gameObject);
        }

        private void OnDetected(Health health)
        {
            AttackAndHealth.Attacker.StartAttack(health);
        }
    }
}