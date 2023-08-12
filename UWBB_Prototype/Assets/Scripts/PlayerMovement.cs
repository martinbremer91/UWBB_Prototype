using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    [SerializeField] private PlayerCameraController camController;
    public Transform playerModel;
    
    public float speed = 5;

    public MovementStyle movementStyle;
    
    private void Update()
    {
        if (movementStyle is MovementStyle.DedicatedVertical)
        {
            HandleXZMovement(inputController.inputState.xzInput);
            HandleYMovementAndRotation(inputController.inputState.yInput);
        } else if (movementStyle is MovementStyle.LockedToCamPlane)
        {
            HandleXZMovement_Locked(inputController.inputState.xzInput);
            HandleYMovementAndRotation_Locked(inputController.inputState.yInput);
        }
    }

    #region DedicatedVertical

    private void HandleXZMovement(Vector2 input)
    {
        Vector3 movementVector = camController.GetVectorInRelationToCamRotation(input);
        transform.Translate(movementVector * (speed * Time.deltaTime), Space.World);
        playerModel.LookAt(transform.position + movementVector);
    }

    private void HandleYMovementAndRotation(Vector2 input)
    {
        transform.Rotate(new Vector3(0, input.x, 0) * (180 * Time.deltaTime));
        transform.Translate(new Vector3(0, input.y, 0) * (speed * Time.deltaTime));
    }
    
    #endregion

    #region LockedToCamPlane

    private void HandleXZMovement_Locked(Vector2 input)
    {
        Vector3 movementVector = camController.GetVectorInRelationToCamRotation(input);
        transform.Translate(movementVector * (speed * Time.deltaTime), Space.World);
        playerModel.LookAt(transform.position + movementVector);
    }

    private void HandleYMovementAndRotation_Locked(Vector2 input)
    {
        // playerModel.Rotate(new Vector3(0, input.x, 0) * (180 * Time.deltaTime), Space.Self);
    }

    #endregion
}

public enum MovementStyle
{
    DedicatedVertical,
    LockedToCamPlane,
}