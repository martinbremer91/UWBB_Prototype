using UnityEngine;

public class AbzuInputController : MonoBehaviour
{
    private AbzuControls controls;

    public AbzuInputState inputState = new();

    private void Awake()
    {
        controls = new();
        controls.Enable();
        AbzuControls.FreeMovementActions freeMovement = controls.FreeMovement;

        freeMovement.LeftStick.performed += context => HandleMoveDirectionInput(context.ReadValue<Vector2>());
        // freeMovement.LeftStick.canceled += context => HandleMoveDirectionInput(context.ReadValue<Vector2>());

        freeMovement.WASD.performed += context => HandleMoveDirectionInput(context.ReadValue<Vector2>());
        freeMovement.WASD.performed += context => HandleMoveDirectionInput(context.ReadValue<Vector2>());

        freeMovement.RightStick.performed += context => HandleCameraAngleInput(context.ReadValue<Vector2>());
        freeMovement.RightStick.canceled += context => HandleCameraAngleInput(context.ReadValue<Vector2>());

        freeMovement.MouseMove.performed += context => HandleCameraAngleInput(context.ReadValue<Vector2>());
        freeMovement.MouseMove.canceled += context => HandleCameraAngleInput(context.ReadValue<Vector2>());
        
        // freeMovement.SnapToHorizon.performed += _ => SnapToHorizon();
        // freeMovement.LockOnToggle.performed += _ => ToggleLockOn();
    }
    
    private void HandleMoveDirectionInput(Vector2 input) => inputState.moveDirectionInput = input;
    private void HandleCameraAngleInput(Vector2 input) => inputState.cameraAngleInput = input;
    
    // private void SnapToHorizon() => inputState.snapCommand = true;
    // private void ToggleLockOn() => inputState.lockOnToggleCommand = true;
}

public struct AbzuInputState
{
    public Vector2 moveDirectionInput;
    public Vector2 cameraAngleInput;
}
