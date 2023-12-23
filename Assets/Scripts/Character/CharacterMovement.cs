using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _haight;
    private float _startJumpVelocity;

    private float _gravityForce;
    private Vector2 _velocity = Vector2.zero;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        float maxHeightTime = _jumpDuration / 2;
        _gravityForce = (2 * _haight) / Mathf.Pow(maxHeightTime, 2);
        _startJumpVelocity = (2 * _haight) / maxHeightTime;
    }

    private void Update()
    {
        GravityHandling();

        Jump();

        Move();
    }

    private void GravityHandling()
    {
        if (_characterController.isGrounded == false)
            _velocity.y -= _gravityForce * Time.fixedDeltaTime;
        else
            _velocity.y = -1f;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
            _velocity.y = _startJumpVelocity;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        _velocity.x = horizontal * _moveSpeed;
        _characterController.Move(_velocity * Time.fixedDeltaTime);

        if (horizontal > 0)
            _spriteRenderer.flipX = false;

        if (horizontal < 0)
            _spriteRenderer.flipX = true;

        _animator.SetFloat("Speed", Mathf.Abs(_velocity.x));
    }
}