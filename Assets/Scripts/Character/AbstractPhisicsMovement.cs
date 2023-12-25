using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public abstract class AbstractPhisicsMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _haight;

    private CharacterController _characterController;

    private float _gravityForce;
    private float _startJumpVelocity;
    private Vector2 _velocity = Vector2.zero;

    protected abstract event UnityAction Jumping;

    protected bool IsGrounded => _characterController.isGrounded;

    public float Direction => _velocity.x;
    public float Speed => Mathf.Abs(_velocity.x);

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        CalculatePhysicalParameters();
    }

    private void OnEnable()
    {
        Jumping += Jump;
    }

    private void OnDisable()
    {
        Jumping -= Jump;
    }

    private void Update()
    {
        GravityHandling();

        Updating();

        Move();
    }

    protected abstract float GetHorizontalDirection();

    protected abstract void Updating();

    private void CalculatePhysicalParameters()
    {
        float jumpDurationFactor = 2.0f;

        float maxHeightTime = _jumpDuration / jumpDurationFactor;
        _gravityForce = (jumpDurationFactor * _haight) / (maxHeightTime * maxHeightTime);
        _startJumpVelocity = (jumpDurationFactor * _haight) / maxHeightTime;
    }

    private void GravityHandling()
    {
        float slightAttractionValue = -1f;

        if (_characterController.isGrounded == false)
            _velocity.y -= _gravityForce * Time.fixedDeltaTime;
        else
            _velocity.y = slightAttractionValue;
    }

    private void Jump()
    {
        if (_characterController.isGrounded == false)
            throw new System.InvalidOperationException();

        _velocity.y = _startJumpVelocity;
    }

    private void Move()
    {
        float horizontal = GetHorizontalDirection();

        _velocity.x = horizontal * _moveSpeed;
        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }
}