using UnityEngine;

namespace UWBB.CharacterController
{
    public class PlayerInputLogic
    {
        private readonly DefaultControls controls = new();
        public InputState inputState;
        
        public void Init()
        {
            controls.Enable();
            
            DefaultControls.FreeMovementActions freeMovement = controls.FreeMovement;

            freeMovement.XZMovement.performed += context => HandleXZInput(context.ReadValue<Vector2>());
            freeMovement.XZMovement.canceled += context => HandleXZInput(context.ReadValue<Vector2>());

            freeMovement.YMovement.performed += context => HandleYInput(context.ReadValue<float>());
            freeMovement.YMovement.canceled += context => HandleYInput(context.ReadValue<float>());

            freeMovement.CameraStick.performed += context => HandleCameraStick(context.ReadValue<Vector2>());
            freeMovement.CameraStick.canceled += context => HandleCameraStick(context.ReadValue<Vector2>());

            inputState.actions = freeMovement;
        }

        public void Deinit() => controls.Disable();

        private void HandleXZInput(Vector2 input) => inputState.characterPlaneInput = input;
        private void HandleYInput(float input) => inputState.worldYInput = Mathf.RoundToInt(input);
        private void HandleCameraStick(Vector2 input) => inputState.characterAxisInput = input;
    }

    public struct InputState
    {
        public DefaultControls.FreeMovementActions actions;
        
        public Vector2 characterPlaneInput;
        public int worldYInput;
        public Vector2 characterAxisInput;

        public bool dashCommand => actions.Dash.WasPressedThisFrame();
        public bool snapCommand => actions.SnapToHorizon.WasPressedThisFrame();
        public bool lockOnCommand => actions.LockOnCommand.WasPressedThisFrame();
    }
}
