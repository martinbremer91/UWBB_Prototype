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

            freeMovement.YMovementandRotation.performed += context => HandleYInput(context.ReadValue<Vector2>());
            freeMovement.YMovementandRotation.canceled += context => HandleYInput(context.ReadValue<Vector2>());

            // freeMovement.SnapToHorizon.started += _ => SnapToHorizon();
            // freeMovement.SnapToHorizon.performed += _ => ReleaseSnapToHorizon();
            // freeMovement.SnapToHorizon.canceled += _ => ReleaseSnapToHorizon();
            
            // freeMovement.LockOnToggle.started += _ => LockOnCommand();
            // freeMovement.LockOnToggle.performed += _ => ReleaseLockOnCommand();
            // freeMovement.LockOnToggle.canceled += _ => ReleaseLockOnCommand();

            inputState.actions = freeMovement;
        }

        public void Deinit() => controls.Disable();

        IInputState IInputLogic<IInputState>.GetInputState() 
            => (this as IInputLogic<FirstVersionInputState>).GetInputState();

        FirstVersionInputState IInputLogic<FirstVersionInputState>.GetInputState()
        {
            DebugPanel.CustomDebug(
                $"FirstVersion\nMovement = {inputState.characterPlaneInput}\n" +
                $"Camera = {inputState.characterAxisInput}\nSnap: " + inputState.snapCommand, DebugFlags.Input);
            return inputState;
        }
        
        private void HandleXZInput(Vector2 input) => inputState.characterPlaneInput = input;
        private void HandleYInput(Vector2 input) => inputState.characterAxisInput = input;
    }

    public struct FirstVersionInputState : IInputState
    {
        public FirstVersionControls.FreeMovementActions actions;
        
        public Vector2 characterPlaneInput;
        public Vector2 characterAxisInput;

        // public bool snapCommand;
        // public bool lockOnCommand;

        public bool snapCommand => actions.SnapToHorizon.WasPressedThisFrame();
        public bool lockOnCommand => actions.LockOnCommand.WasPressedThisFrame();
    }
}
