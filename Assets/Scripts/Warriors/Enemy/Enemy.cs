using CharacterSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(JumpMovementFacade))]
    [RequireComponent(typeof(TeleportAnimationHandler))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour, IDieable
    {
        [SerializeField] private AbstractDetector _detector;
        [SerializeField] private StateMachine _movementStateMachine;

        [SerializeField] private PatrollPath _patrollPath;

        [SerializeField] private float _maxAttackDistanceByXAxis;
        [SerializeField] private float _maxAttackDistanceByYAxis;

        private TeleportAnimationHandler _teleport;
        private Transform _selfTransform;
        private Animator _animator;

        [field: SerializeField] public AttackAndHealthFacade AttackAndHealth { get; private set; }

        public Movement Movement { get; private set; }
        public MoveAnimation MoveAnimation { get; private set; }

        public AbstractDetector Detector => _detector;
        public PatrollPath PatrollPath => _patrollPath;

        public event UnityAction Died;

        public void Initialize(Character character, UpdateServise updateServise)
        {
            JumpMovementFacade jumpMovementFacade = GetComponent<JumpMovementFacade>();
            _teleport = GetComponent<TeleportAnimationHandler>();
            _animator = GetComponent<Animator>();
            _selfTransform = transform;

            _teleport.Showed += Respawn;
            Movement = new Movement(_selfTransform, jumpMovementFacade, updateServise);
            MoveAnimation = new MoveAnimation(Movement, _animator, _selfTransform, updateServise);

            jumpMovementFacade.Initialize(updateServise);
            AttackAndHealth.Initialize(updateServise, this);
            _movementStateMachine.Initialize(this, character, updateServise);

            Respawn();
        }

        private void OnDisable()
        {
            _teleport.Showed -= Respawn;
        }

        void IDieable.Die()
        {
            _movementStateMachine.Dispose();
            Movement.Dispose();

            Died?.Invoke();
            gameObject.SetActive(false);
        }

        public bool CanAttack(Vector2 targetPosition)
        {
            return DirectionConst.GetDistance(_selfTransform.position.x, targetPosition.x) <= _maxAttackDistanceByXAxis
                && DirectionConst.GetDistance(_selfTransform.transform.position.y, targetPosition.y) <= _maxAttackDistanceByYAxis;
        }

        private void Respawn()
        {
            _patrollPath.Reset();
            Movement.Respawn(_patrollPath.GetNextPathPoint().position);
            _movementStateMachine.ChangeState<PatrolleState>();
        }
    }
}