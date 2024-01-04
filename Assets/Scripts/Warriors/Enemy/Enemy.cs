using CharacterSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(JumpMovementFacade))]
    public class Enemy : MonoBehaviour, IDieable
    {
        [SerializeField] private AbstractDetector _detector;
        [SerializeField] private StateMachine _movementStateMachine;

        [SerializeField] private PatrollPath _patrollPath;

        [SerializeField] private float _maxAttackDistanceByXAxis;
        [SerializeField] private float _maxAttackDistanceByYAxis;

        private Transform _selfTransform;

        [field: SerializeField] public AttackAndHealthFacade AttackAndHealth { get; private set; }

        public Movement Movement { get; private set; }
        public MoveAnimation MoveAnimation { get; private set; }

        public AbstractDetector Detector => _detector;
        public PatrollPath PatrollPath => _patrollPath;

        public event UnityAction Died;

        public void Initialize(Character character, UpdateServise updateServise)
        {
            JumpMovementFacade jumpMovementFacade = GetComponent<JumpMovementFacade>();
            Animator animator = GetComponent<Animator>();
            _selfTransform = transform;

            Movement = new Movement(_selfTransform, jumpMovementFacade, updateServise);
            MoveAnimation = new MoveAnimation(Movement, animator, _selfTransform, updateServise);

            jumpMovementFacade.Initialize(updateServise);
            AttackAndHealth.Initialize(updateServise, this);
            _movementStateMachine.Initialize(this, character, updateServise);
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

        public void TeleportToStartPoint(bool isNeedShowAnimaton = true)
        {
            gameObject.SetActive(true);

            StartCoroutine(Teleport(isNeedShowAnimaton));
        }

        private IEnumerator Teleport(bool isNeedShowAnimaton)
        {
            if(isNeedShowAnimaton)
            {
                MoveAnimation.PlayTeleportAnimation();
                yield return new WaitForSeconds(1f);
            }

            _patrollPath.Reset();
            Movement.Respawn(_patrollPath.GetNextPathPoint().position);
            _movementStateMachine.ChangeState<PatrolleState>();
        }
    }
}