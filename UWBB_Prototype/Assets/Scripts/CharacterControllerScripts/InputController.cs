using UnityEngine;

namespace UWBB.CharacterController
{
    public class InputController : MonoBehaviour
    {
        private void Awake() {
        
            /*
        controls.Enable();
        AbzuControls.FreeMovementActions freeMovement = controls.FreeMovement;

        freeMovement.WASD.performed += context => HandleMoveDirectionInput(context.ReadValue<Vector2>());
        freeMovement.WASD.performed += context => HandleMoveDirectionInput(context.ReadValue<Vector2>());

        freeMovement.MouseMove.performed += context => HandleCameraAngleInput(context.ReadValue<Vector2>());
        freeMovement.MouseMove.canceled += context => HandleCameraAngleInput(context.ReadValue<Vector2>());
        */
        
            // freeMovement.SnapToHorizon.performed += _ => SnapToHorizon();
            // freeMovement.LockOnToggle.performed += _ => ToggleLockOn();
        }

        /*
    private void HandleMoveDirectionInput(Vector2 input) => inputState.moveDirectionInput = input;
    private void HandleCameraAngleInput(Vector2 input) => inputState.cameraAngleInput = input;
    */

        // private void SnapToHorizon() => inputState.snapCommand = true;
        // private void ToggleLockOn() => inputState.lockOnToggleCommand = true;
    }
}