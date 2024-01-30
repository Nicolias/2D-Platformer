using UnityEngine;

namespace CharacterNamespace
{
    public class Input : AbstractInput
    {
        private readonly IUpdateable _jumpInput;
        private readonly IUpdateable _movementInput;

        public Input(Movement movementController)
        {
            _jumpInput = new JumpInput(movementController);
            _movementInput = new MovementInput(movementController);
        }

        protected override void Update(float timeBetweenFrame)
        {
            _jumpInput.Update(timeBetweenFrame);
            _movementInput.Update(timeBetweenFrame);            
        }

        private class MovementInput : IUpdateable
        {
            private readonly Movement _movement;

            public MovementInput(Movement jumpMovementFacade)
            {
                _movement = jumpMovementFacade;
            }

            public void Update(float timeBetweenFrame)
            {
                _movement.Move(UnityEngine.Input.GetAxis(InputConst.Horizontal), timeBetweenFrame);
            }
        }

        private class JumpInput : IUpdateable
        {
            private readonly Movement _movement;

            public JumpInput(Movement jumpMovementFacade)
            {
                _movement = jumpMovementFacade;
            }

            public void Update(float timeBetweenFrame)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                    _movement.TryJump();
            }
        }
    }
}