﻿using UnityEngine;

namespace CharacterSystem
{
    public class Input
    {
        public Input(JumpMovementFacade jumpMovementFacade, UpdateServise updateServise)
        {
            updateServise.AddToFixedUpdate(new MovementInput(jumpMovementFacade));
            updateServise.AddToUpdate(new JumpInput(jumpMovementFacade));
        }

        private class MovementInput : IUpdateable
        {
            private readonly JumpMovementFacade _movement;

            public MovementInput(JumpMovementFacade jumpMovementFacade)
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
            private readonly JumpMovementFacade _movement;

            public JumpInput(JumpMovementFacade jumpMovementFacade)
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