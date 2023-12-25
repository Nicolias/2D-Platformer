using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : AbstractPhisicsMovement
{
    protected override event UnityAction Jumping;

    protected override float GetHorizontalDirection()
    {
        return Input.GetAxis(InputConst.Horizontal);
    }

    protected override void Updating()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            Jumping?.Invoke();
    }
}