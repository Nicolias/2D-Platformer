using UnityEngine;

namespace Character
{
    public class Input : IFixedUpdateable
    {
        private readonly IFixedUpdateable _jumpInput;
        private readonly IFixedUpdateable _movementInput;

        public Input(MovementController movementController)
        {
            _jumpInput = new JumpInput(movementController);
            _movementInput = new MovementInput(movementController);
        }

        void IFixedUpdateable.Update(float timeBetweenFrame)
        {
            _jumpInput.Update(timeBetweenFrame);
            _movementInput.Update(timeBetweenFrame);            
        }

        private class MovementInput : IFixedUpdateable
        {
            private readonly MovementController _movement;

            public MovementInput(MovementController jumpMovementFacade)
            {
                _movement = jumpMovementFacade;
            }

            void IFixedUpdateable.Update(float timeBetweenFrame)
            {
                _movement.Move(UnityEngine.Input.GetAxis(InputConst.Horizontal), timeBetweenFrame);
            }
        }

        private class JumpInput : IFixedUpdateable
        {
            private readonly MovementController _movement;

            public JumpInput(MovementController jumpMovementFacade)
            {
                _movement = jumpMovementFacade;
            }

            void IFixedUpdateable.Update(float timeBetweenFrame)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                    _movement.TryJump();
            }
        }
    }
}