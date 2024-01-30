using System;
using UnityEngine;

public class Movement : IMoveable, IFixedUpdateable
{
    private readonly Velocity _velocity;

    private readonly PhisicsMovement _phisicsMovement;
    private readonly Jumper _jumper;

    public Movement(CharacterController characterController, float moveSpeed, float jumpDuration, float jumpHaight)
    {
        _velocity = new Velocity();

        _phisicsMovement = new PhisicsMovement(_velocity, characterController, moveSpeed);
        _jumper = new Jumper(_velocity, characterController, jumpDuration, jumpHaight);
    }

    public DirectionType Direction => DirectionConst.GetDirectionType(Mathf.Clamp(_velocity.X, DirectionConst.Left, DirectionConst.Right));
    public float Speed => MathF.Abs(_velocity.X);

    void IFixedUpdateable.Update(float timeBetweenFrame)
    {
        _jumper.GravityHandling(timeBetweenFrame);
    }

    public void TryJump()
    {
        _jumper.TryJump();
    }

    public void Move(float direction, float frameDeltaTime)
    {
        _phisicsMovement.Move(DirectionConst.Clamp(direction), frameDeltaTime);
    }

    public void Teleport(Vector2 position)
    {
        _phisicsMovement.Teleport(position);
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

        public void Move(float directionX, float frameDeltaTime)
        {
            if (directionX < DirectionConst.Left && directionX > DirectionConst.Right)
                throw new ArgumentOutOfRangeException($"Ќаправление должно быть в пределах от {DirectionConst.Left} до {DirectionConst.Right}");

            _velocity.X = directionX * _speed;
            _characterController.Move(new Vector2(_velocity.X * frameDeltaTime, _velocity.Y * frameDeltaTime));
        }

        public void Teleport(Vector2 position)
        {
            _characterController.enabled = false;
            _characterController.transform.position = position;
            _characterController.enabled = true;
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