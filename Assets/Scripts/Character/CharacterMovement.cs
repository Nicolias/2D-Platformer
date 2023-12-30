using UnityEngine;

[RequireComponent(typeof(JumpMovementFacade))]
public class CharacterMovement : MonoBehaviour
{
    private JumpMovementFacade _movement;

    private void Awake()
    {
        _movement = GetComponent<JumpMovementFacade>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _movement.TryJump();

        _movement.Move(Input.GetAxis(InputConst.Horizontal), Time.deltaTime);
    }
}