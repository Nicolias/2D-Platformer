using System;
using UnityEngine;

public class WarriarAnimationHandler : IUpdateable, IDisposable
{
    private const float HalthRotationAngel = 180;

    private readonly Quaternion _rightRoration = Quaternion.identity;
    private readonly Quaternion _leftRotation = Quaternion.Euler(0, HalthRotationAngel, 0);

    private readonly IMoveable _movement;
    private readonly Animator _animator;
    private readonly Transform _selfTransform;
    private readonly UpdateServise _updateServise;

    public WarriarAnimationHandler(IMoveable movement, Animator animator, Transform transform, UpdateServise updateServise)
    {
        if (movement == null) throw new ArgumentNullException();
        if (animator == null) throw new ArgumentNullException();
        if (transform == null) throw new ArgumentNullException();
        if (updateServise == null) throw new ArgumentNullException();

        _movement = movement;
        _animator = animator;
        _selfTransform = transform;
        _updateServise = updateServise;

        updateServise.AddToUpdate(this);
    }

    public Animator Animator => _animator;

    public void Dispose()
    {
        _updateServise.RemoveFromUpdate(this);
    }

    void IUpdateable.Update(float timeBetweenFrame)
    {
        if (_movement != null)
        {
            Rotate(_movement.Direction);
            _animator.SetFloat(AnimatorData.Params.Speed, _movement.Speed); 
        }
    }

    public void PlayTeleportAnimation()
    {
        _animator.SetTrigger(AnimatorData.Params.Teleport);
    }

    private void Rotate(DirectionType direction)
    {
        if (direction == DirectionType.Right)
            _selfTransform.rotation = _rightRoration;
        if (direction == DirectionType.Left)
            _selfTransform.rotation = _leftRotation;
    }
}