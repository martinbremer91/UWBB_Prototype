using UnityEngine;

public class MovementInputController : MonoBehaviour
{
    private DefaultMovement defaultMovement;

    public InputState inputState = new();

    private void Awake()
    {
        defaultMovement = new();
        defaultMovement.Enable();
        DefaultMovement.FreeMovementActions freeMovement = defaultMovement.FreeMovement;

        freeMovement.XZMovement.performed += context => HandleXZInput(context.ReadValue<Vector2>());
        freeMovement.XZMovement.canceled += context => HandleXZInput(context.ReadValue<Vector2>());

        freeMovement.YMovementandRotation.performed += context => HandleYInput(context.ReadValue<Vector2>());
        freeMovement.YMovementandRotation.canceled += context => HandleYInput(context.ReadValue<Vector2>());

        freeMovement.SnapToHorizon.performed += _ => SnapToHorizon();
        freeMovement.LockOnToggle.performed += _ => ToggleLockOn();
    }
    
    private void HandleXZInput(Vector2 input) => inputState.xzInput = input;
    private void HandleYInput(Vector2 input) => inputState.yInput = input;
    private void SnapToHorizon() => inputState.snapCommand = true;
    private void ToggleLockOn() => inputState.lockOnToggleCommand = true;
}

public struct InputState
{
    public Vector2 xzInput;
    public Vector2 yInput;

    public bool snapCommand;
    public bool lockOnToggleCommand;
}