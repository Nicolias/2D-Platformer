using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class JumpMovementFacade : MonoBehaviour, IMovement
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHaight;

    private Velocity _velocity;
    private PhisicsMovement _phisicsMovement;
    private Jumper _jumper;

    private void Awake()
    {
        CharacterController characterController = GetComponent<CharacterController>();

        _velocity = new Velocity();

        _phisicsMovement = new PhisicsMovement(_velocity, characterController, _moveSpeed);
        _jumper = new Jumper(_velocity, characterController, _jumpDuration, _jumpHaight);
    }

    public float Direction => _velocity.X;
    public float Speed => Mathf.Abs(_velocity.X);

    private void FixedUpdate()
    {
        _jumper.GravityHandling(Time.fixedDeltaTime);
    }

    public void TryJump()
    {
        _jumper.TryJump();
    }

    public void Move(float direction, float frameDeltaTime)
    {
        _phisicsMovement.Move(DirectionConst.GetDirectionNormolized(direction), frameDeltaTime);
    }

    private class PhisicsMovement
    {
        private readonly CharacterController _characterController;
        private readonly float _speed;

        private Velocity _velocity;

        public PhisicsMovement(Velocity velocity, CharacterController characterController, float speed)
        {
            _velocity = velocity;

            _characterController = characterController;
            _speed = speed;
        }

        public void Move(float direction, float frameDeltaTime)
        {
            if (direction < DirectionConst.Left && direction > DirectionConst.Right)
                throw new ArgumentException($"Ќаправление должно быть в пределах от {DirectionConst.Left} до {DirectionConst.Right}");

            _velocity.X = direction * _speed;
            _characterController.Move(new Vector2(_velocity.X * frameDeltaTime, _velocity.Y * frameDeltaTime));
        }
    }

    private class Jumper
    {
        private readonly Velocity _velocity;
        private readonly CharacterController _characterController;

        private float _gravityForce;
        private float _startJumpVelocity;

        public Jumper(Velocity velocity, CharacterController characterController, float duration, float haight)
        {
            _velocity = velocity;
            _characterController = characterController;

            CalculatePhysicalParameters(duration, haight);
        }

        public void TryJump()
        {
            if (_characterController.isGrounded)
                _velocity.Y = _startJumpVelocity;
        }

        public void GravityHandling(float frameDeltaTime)
        {
            float slightAttractionValue = -1f;

            if (_characterController.isGrounded == false)
                _velocity.Y -= _gravityForce * frameDeltaTime;
            else
                _velocity.Y = slightAttractionValue;
        }

        private void CalculatePhysicalParameters(float duration, float haight)
        {
            float jumpDurationFactor = 2.0f;

            float maxHeightTime = duration / jumpDurationFactor;
            _gravityForce = (jumpDurationFactor * haight) / (maxHeightTime * maxHeightTime);
            _startJumpVelocity = (jumpDurationFactor * haight) / maxHeightTime;
        }
    }

    private class Velocity
    {
        public float Y;
        public float X;
    }
}