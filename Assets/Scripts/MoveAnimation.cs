﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class MoveAnimation : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movement;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private IMovement _iMovement => (IMovement) _movement;

    private void OnValidate()
    {
        if (_movement is IMovement)
            return;

        Debug.LogError(_movement.name + " needs to implement " + nameof(IMovement));
        _movement = null;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetRotation(_iMovement.Direction);
        _animator.SetFloat(AnimatorData.Params.Speed, _iMovement.Speed);
    }

    private void SetRotation(float direction)
    {
        int halfTurnValue = 180;

        if (direction > 0)
            transform.rotation = new Quaternion(0, 0, 0, 0);

        if (direction < 0)
            transform.rotation = new Quaternion(0, halfTurnValue, 0, 0);
    }
}