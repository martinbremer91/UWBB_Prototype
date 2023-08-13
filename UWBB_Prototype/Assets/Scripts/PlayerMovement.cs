using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    [SerializeField] private PlayerCameraController camController;
    [SerializeField] private PlayerLockOnController lockOnController;
    public Transform playerModel;
    
    public float speed = 5;

    public MovementStyle movementStyle;
    
    private void Update()
    {
        if (lockOnController.lockedOn)
            HandleXZLockedMovement(inputController.inputState.xzInput);
        else if (movementStyle is MovementStyle.DedicatedVertical)
        {
            HandleXZFreeMovement(inputController.inputState.xzInput);
            HandleYMovementAndRotation(inputController.inputState.yInput);
        } else if (movementStyle is MovementStyle.LockedToCamPlane)
        {
            HandleXZMovement_Locked(inputController.inputState.xzInput);
        }
    }

    #region DedicatedVertical

    private void HandleXZFreeMovement(Vector2 input)
    {
        Vector3 movementVector = camController.GetVectorInRelationToCamRotation(input);
        transform.Translate(movementVector * (speed * Time.deltaTime), Space.World);
        SetModelLookAtTarget(transform.position + movementVector);
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
        SetModelLookAtTarget(transform.position + movementVector);
    }

    #endregion

    #region LockedOnMovement
    
    // TODO: figure out how to convert speed to angles here
    private void HandleXZLockedMovement(Vector2 input)
    {
        transform.RotateAround(lockOnController.target.position, Vector3.up, -input.x * 60 * Time.deltaTime);
        
        Vector3 movementVector = camController.transform.forward * input.y;
        transform.Translate(movementVector * (speed * Time.deltaTime), Space.World);

        SetModelLookAtTarget(lockOnController.target.position);
    }

    #endregion

    private void SetModelLookAtTarget(Vector3 target) => playerModel.LookAt(target);
}

public enum MovementStyle
{
    DedicatedVertical,
    LockedToCamPlane,
}