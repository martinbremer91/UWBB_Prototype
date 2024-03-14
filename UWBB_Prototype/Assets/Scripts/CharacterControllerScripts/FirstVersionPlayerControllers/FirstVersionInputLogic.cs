using MBre.Utilities;
using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionInputLogic : 
        IInputLogic<IInputState>, 
        IInputLogic<FirstVersionInputState>
    {
        private readonly FirstVersionControls controls = new();
        private FirstVersionInputState inputState;
        
        public void Init()
        {
            controls.Enable();
            
            FirstVersionControls.FreeMovementActions freeMovement = controls.FreeMovement;

            freeMovement.XZMovement.performed += context => HandleXZInput(context.ReadValue<Vector2>());
            freeMovement.XZMovement.canceled += context => HandleXZInput(context.ReadValue<Vector2>());

            freeMovement.YMovement.performed += context => HandleYInput(context.ReadValue<float>());
            freeMovement.YMovement.canceled += context => HandleYInput(context.ReadValue<float>());

            freeMovement.CameraStick.performed += context => HandleCameraStick(context.ReadValue<Vector2>());
            freeMovement.CameraStick.canceled += context => HandleCameraStick(context.ReadValue<Vector2>());

            inputState.actions = freeMovement;
        }

        public void Deinit() => controls.Disable();

        IInputState IInputLogic<IInputState>.GetInputState() 
            => (this as IInputLogic<FirstVersionInputState>).GetInputState();

        FirstVersionInputState IInputLogic<FirstVersionInputState>.GetInputState() => inputState;

        private void HandleXZInput(Vector2 input) => inputState.characterPlaneInput = input;
        private void HandleYInput(float input) => inputState.worldYInput = Mathf.RoundToInt(input);
        private void HandleCameraStick(Vector2 input) => inputState.characterAxisInput = input;
    }

    public struct FirstVersionInputState : IInputState
    {
        public FirstVersionControls.FreeMovementActions actions;
        
        public Vector2 characterPlaneInput;
        public int worldYInput;
        public Vector2 characterAxisInput;

        public bool dashCommand => actions.Dash.WasPressedThisFrame();
        public bool snapCommand => actions.SnapToHorizon.WasPressedThisFrame();
        public bool lockOnCommand => actions.LockOnCommand.WasPressedThisFrame();
    }
}
