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

            freeMovement.SnapToHorizon.performed += _ => SnapToHorizon();
            freeMovement.LockOnToggle.performed += _ => ToggleLockOn();
        }

        public void Deinit() => controls.Disable();

        IInputState IInputLogic<IInputState>.GetInputState() 
            => (this as IInputLogic<FirstVersionInputState>).GetInputState();

        FirstVersionInputState IInputLogic<FirstVersionInputState>.GetInputState()
        {
            // TODO: debug panel call
            return inputState;
        }
        
        private void HandleXZInput(Vector2 input) => inputState.characterPlaneInput = input;
        private void HandleYInput(Vector2 input) => inputState.characterAxisInput = input;
        private void SnapToHorizon() => inputState.snapCommand = true;
        private void ToggleLockOn() => inputState.lockOnToggleCommand = true;
    }

    public struct FirstVersionInputState : IInputState
    {
        public Vector2 characterPlaneInput;
        public Vector2 characterAxisInput;

        public bool snapCommand;
        public bool lockOnToggleCommand;
    }
}
