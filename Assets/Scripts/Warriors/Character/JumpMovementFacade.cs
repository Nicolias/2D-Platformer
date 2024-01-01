using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class JumpMovementFacade : MonoBehaviour, IMoveable, IUpdateable, IDisposable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHaight;

    private CharacterController _characterController;
    private UpdateServise _updateServise;

    private Velocity _velocity;
    private PhisicsMovement _phisicsMovement;
    private Jumper _jumper;

    public float Direction => _velocity.X;
    public float Speed => Mathf.Abs(_velocity.X);

    public void Initialize(UpdateServise updateServise)
    {
        _characterController = GetComponent<CharacterController>();
        _updateServise = updateServise;
        updateServise.AddToFixedUpdate(this);

        _velocity = new Velocity();

        _phisicsMovement = new PhisicsMovement(_velocity, _characterController, _moveSpeed);
        _jumper = new Jumper(_velocity, _characterController, _jumpDuration, _jumpHaight);
    }

    public void Dispose()
    {
        _updateServise.RemoveFromFixedUpdate(this);
    }

    void IUpdateable.Update(float timeBetweenFrame)
    {
        _jumper.GravityHandling(timeBetweenFrame);
    }

    public void TryJump()
    {
        _jumper.TryJump();
    }

    public void Move(float direction, float frameDeltaTime)
    {
        _phisicsMovement.Move(DirectionConst.GetDirectionNormolized(direction), frameDeltaTime);
    }

    public void Teleport(Vector2 position)
    {
        _characterController.enabled = false;
        _characterController.transform.position = position;
        _characterController.enabled = true;
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