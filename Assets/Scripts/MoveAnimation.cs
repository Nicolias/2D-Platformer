using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class MoveAnimation : MonoBehaviour
{
    private IMovement _movement;
    private Animator _animator;

    public void Initialize(IMovement movement)
    {
        _movement = movement;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Set(_movement.Direction, _movement.Speed);
    }

    private void Set(float direction, float speed)
    {
        int halfTurnValue = 180;

        if (direction > 0)
            transform.rotation = new Quaternion(0, 0, 0, 0);

        if (direction < 0)
            transform.rotation = new Quaternion(0, halfTurnValue, 0, 0);

        _animator.SetFloat(AnimatorData.Params.Speed, speed);
    }

    internal void Initialize(object currentMovement)
    {
        throw new NotImplementedException();
    }
}