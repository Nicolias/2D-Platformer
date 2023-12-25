using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _haight;

    private CharacterController _characterController;

    private float _gravityForce;
    private float _startJumpVelocity;
    private Vector2 _velocity = Vector2.zero;

    public float Direction => _velocity.x;
    public float Speed => Mathf.Abs(_velocity.x);

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        CalculatePhysicalParameters();
    }

    private void Update()
    {
        GravityHandling();

        Jump();

        Move();
    }

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
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
            _velocity.y = _startJumpVelocity;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(InputConst.Horizontal);

        _velocity.x = horizontal * _moveSpeed;
        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }
}
