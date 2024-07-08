using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Input
    {
        private readonly DefaultControls controls = new();
        private DefaultControls.GameplayActions gameplayActions;

        public CharacterController_Input()
        {
            gameplayActions = controls.Gameplay;
            
            controls.Enable();
            gameplayActions.Enable();
        }

        public void Update(InputState inputState)
        {
            inputState.cameraAim = gameplayActions.CameraAim.ReadValue<Vector2>();
            inputState.moveDirection = gameplayActions.MovementDirection.ReadValue<Vector2>();
        }
    }

    public class InputState
    {
        public Vector2 cameraAim;
        public Vector2 moveDirection;
    }
}
