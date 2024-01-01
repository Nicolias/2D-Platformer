using System;
using UnityEngine;

public class MoveAnimation : IUpdateable, IDisposable
{
    private readonly IMovement _movement;
    private readonly Animator _animator;
    private readonly Transform _selfTransform;
    private readonly UpdateServise _updateServise;

    public MoveAnimation(IMovement movement, Animator animator, Transform transform, UpdateServise updateServise)
    {
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

    private void Rotate(float direction)
    {
        int halfTurnValue = 180;

        if (direction > 0)
            _selfTransform.rotation = new Quaternion(0, 0, 0, 0);

        if (direction < 0)
            _selfTransform.rotation = new Quaternion(0, halfTurnValue, 0, 0);
    }
}