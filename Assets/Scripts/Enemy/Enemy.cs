using CharacterSystem;
using System;
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

        private UpdateServise _updateServise;

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
            _updateServise = updateServise;
            Transform selfTransform = transform;

            Attacker = new Attacker(_damage, _attackCoolDown, animator, updateServise);
            Health = new Health(_health);
            Movement = new Movement(selfTransform, jumpMovementFacade, updateServise);
            MoveAnimation = new MoveAnimation(Movement, animator, selfTransform, updateServise);

            _movementStateMachine.Initialize(this, character, updateServise);
        }

        public void TeleportToStartPoint()
        {
            StartCoroutine(Teleport());
        }

        private IEnumerator Teleport()
        {
            MoveAnimation.PlayTeleportAnimation();

            yield return new WaitForSeconds(1f);
            _patrollPath.Reset();
            Movement.Respawn(_patrollPath.GetNextPathPoint().position);
            _movementStateMachine.ChangeState<PatrolleState>();
        }
    }
}

public class Attacker : IUpdateable
{
    private readonly int _damage;
    private readonly float _attackCooldown;
    private readonly UpdateServise _updateServise;
    private readonly Animator _selfAnimator;

    private float _nextAttackTime;
    private Health _damageable;

    public Attacker(int damage, float attackCooldown, Animator selfAnimator, UpdateServise updateServise)
    {
        _damage = damage;
        _attackCooldown = attackCooldown;
        _updateServise = updateServise;

        _selfAnimator = selfAnimator;
    }

    void IUpdateable.Update(float timeBetweenFrame)
    {
        if (_nextAttackTime <= Time.time)
            Attack();
    }

    public void StartAttack(Health health)
    {
        _damageable = health;

        Attack();
        _updateServise.AddToUpdate(this);
    }

    public void StopAttack()
    {
        _damageable = null;
        _updateServise.RemoveFromUpdate(this);
    }

    private void Attack()
    {
        _damageable.TakeDamage(_damage);
        _nextAttackTime = Time.time + _attackCooldown;

        _selfAnimator.SetTrigger(AnimatorData.Params.Attack);
    }
}

public class Health
{
    private int _healthValue;

    public Health(int healthValue)
    {
        _healthValue = healthValue;
    }

    public event Action Dead;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new InvalidOperationException();

        _healthValue -= damage;

        Debug.Log(_healthValue);

        if (_healthValue < 0)
            Dead?.Invoke();
    }
}