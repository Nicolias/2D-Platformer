using CharacterSystem;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(JumpMovementFacade))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Detector _detector;
        [SerializeField] private StateMachine _movementStateMachine;

        [SerializeField] private PatrollPath _patrollPath;

        [SerializeField] private int _damage;
        [SerializeField] private int _attackCoolDown;

        [SerializeField] private int _health;

        public Attacker Attacker { get; private set; }
        public Health Health { get; private set; }
        public Movement Movement { get; private set; }
        public MoveAnimation MoveAnimation { get; private set; }

        public Detector Detector => _detector;
        public PatrollPath PatrollPath => _patrollPath;

        public void Initialize(Character character, UpdateServise updateServise)
        {
            JumpMovementFacade jumpMovementFacade = GetComponent<JumpMovementFacade>();
            Animator animator = GetComponent<Animator>();
            Transform selfTransform = transform;

            Attacker = new Attacker(_damage, _attackCoolDown, animator, updateServise);
            Health = new Health(_health);
            Movement = new Movement(selfTransform, jumpMovementFacade, updateServise);
            MoveAnimation = new MoveAnimation(Movement, animator, selfTransform, updateServise);

            jumpMovementFacade.Initialize(updateServise);
            _movementStateMachine.Initialize(this, character, updateServise);
        }

        public void TeleportToStartPoint(bool isNeedShowAnimaton = true)
        {
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