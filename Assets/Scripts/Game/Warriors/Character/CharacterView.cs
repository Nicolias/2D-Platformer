using System;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public abstract class AbstractWarriarView : MonoBehaviour
    {
        [SerializeField] private AbstractDetector _detector;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpHaight;

        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private int _attackCoolDown;

        public WarriarModel Model { get; private set; }
        public WarriarAnimation Animation { get; private set; }

        public void Initialize(UpdateServise updateServise)
        {
            gameObject.SetActive(true);

            MovementController movementController = new MovementController(GetComponent<CharacterController>(), _moveSpeed, _jumpDuration, _jumpHaight);

            Animation = new WarriarAnimation(movementController, GetComponent<Animator>(), transform, updateServise);
            Model = CreateModel(movementController, new Health(_health), new Attacker(_damage, _attackCoolDown, updateServise), _detector, updateServise);

            Model.Died += OnDied;
            Model.Attacker.Attacked += OnAttacked;
        }


        private void OnDisable()
        {
            Model.Dispose();

            Model.Died -= OnDied;
            Model.Attacker.Attacked -= OnAttacked;
        }

        protected abstract WarriarModel CreateModel(MovementController movementController, Health health, Attacker attacker, AbstractDetector detector, UpdateServise updateServise);

        private void OnAttacked()
        {
            Animation.Animator.SetTrigger(AnimatorData.Params.Attack);
        }

        private void OnDied()
        {
            Animation.Dispose();
            Model.Dispose();

            Animation.Animator.SetTrigger(AnimatorData.Params.Diy);
            gameObject.SetActive(false);
        }
    }

    public class CharacterView : AbstractWarriarView
    {
        protected override WarriarModel CreateModel(MovementController movementController, Health health, Attacker attacker, AbstractDetector detector, UpdateServise updateServise)
        {
            return new CharacterModel(movementController, health, attacker, detector, updateServise);
        }
    }

    public abstract class WarriarModel : IDisposable
    {

        private readonly AbstractDetector _detector;

        public Attacker Attacker { get; private set; }
        public Health Health { get; private set; }

        protected UpdateServise UpdateServise { get; }
        protected MovementController MovementController { get; }

        public WarriarModel(MovementController movementController, Health health, Attacker attacker, AbstractDetector detector, UpdateServise updateServise)
        {
            if (detector == null)
                throw new ArgumentNullException();

            UpdateServise = updateServise;
            MovementController = movementController;

            _detector = detector;

            Health = health;
            Attacker = attacker;

            InitializeInputSystem();
            UpdateServise.AddToFixedUpdate(movementController);

            Health.Over += Die;

            detector.Detected += OnTargetDetected;
            detector.Lost += OnTargetLost;
        }

        public event UnityAction Died;

        public virtual void Dispose()
        {
            Attacker.StopAttack();
            UpdateServise.RemoveFromFixedUpdate(MovementController);

            Health.Over -= Die;

            _detector.Detected -= OnTargetDetected;
            _detector.Lost -= OnTargetLost;
        }

        protected abstract void InitializeInputSystem();

        protected abstract void OnTargetDetected(Health health);

        protected abstract void OnTargetLost();

        private void Die()
        {
            Dispose();
            Died?.Invoke();
        }
    }

    public class CharacterModel : WarriarModel, IDisposable
    {
        private Input _input;

        public CharacterModel(MovementController movementController, Health health, Attacker attacker, AbstractDetector detector, UpdateServise updateServise) 
            : base(movementController, health, attacker, detector, updateServise)
        {
        }

        public override void Dispose()
        {
            base.Dispose();

            UpdateServise.RemoveFromFixedUpdate(_input);
        }

        protected override void InitializeInputSystem()
        {
            _input = new Input(MovementController);
            UpdateServise.AddToFixedUpdate(_input);
        }

        protected override void OnTargetDetected(Health health)
        {
            Attacker.StartAttack(health);
        }

        protected override void OnTargetLost()
        {
            Attacker.StopAttack();
        }
    }
}