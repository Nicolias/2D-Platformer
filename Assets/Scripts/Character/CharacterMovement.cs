using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _jampDuration;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;

    private float _yVelocity = 0;
    private bool _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float xVelocity = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
            StartCoroutine(Jump());

        _rigidbody.velocity = new Vector2(xVelocity, _yVelocity) * Time.deltaTime * _moveSpeed;
    }

    private IEnumerator Jump()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);
        float timePasted = 0;

        float gravityScale = _rigidbody.gravityScale;

        _isJumping = true;
        _rigidbody.gravityScale = 0;

        _yVelocity = _jumpForce * Time.deltaTime;

        while ( timePasted < _jampDuration)
        {
            yield return waitForSeconds;
            timePasted += 0.01f;
        }

        _yVelocity = 0;
        _rigidbody.gravityScale = gravityScale;

        _isJumping = false;
    }
}