using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    [SerializeField] private PlayerCameraController camController;
    [SerializeField] private PlayerLockOnController lockOnController;
    public Transform playerModel;

    [SerializeField] private CharacterControllerConfigs configs;

    public MovementStyle movementStyle;

    private void Start() => lockOnController.onLockOn += OnLockOn;

    private void Update()
    {
        if (lockOnController.lockedOn)
        {
            HandleXZLockedOnMovement(inputController.inputState.xzInput);
            HandleYLockedOnMovement(inputController.inputState.yInput);
        }
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
        transform.Translate(movementVector * (configs.speed * Time.deltaTime), Space.World);
        SetModelLookAtTarget(transform.position + movementVector);
    }

    private void HandleYMovementAndRotation(Vector2 input)
    {
        transform.Rotate(new Vector3(0, input.x, 0) * (GetLockOnRotationSpeed() * Time.deltaTime));
        transform.Translate(new Vector3(0, input.y, 0) * (configs.speed * Time.deltaTime));
        SetModelLookAtTarget(lockOnController.target.position);
    }
    
    #endregion

    #region LockedToCamPlane

    private void HandleXZMovement_Locked(Vector2 input)
    {
        Vector3 movementVector = camController.GetVectorInRelationToCamRotation(input);
        transform.Translate(movementVector * (configs.speed * Time.deltaTime), Space.World);
        SetModelLookAtTarget(transform.position + movementVector);
    }

    #endregion

    #region LockedOnMovement
    
    // TODO: figure out how to convert speed to angles here
    private void HandleXZLockedOnMovement(Vector2 input)
    {
        transform.RotateAround(lockOnController.target.position, Vector3.up, -input.x * GetLockOnRotationSpeed() * Time.deltaTime);
        
        Vector3 movementVector = camController.transform.forward * input.y;
        transform.Translate(movementVector * (configs.speed * Time.deltaTime), Space.World);
    }

    private void HandleYLockedOnMovement(Vector2 input)
    {
        if (Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * configs.minAngleToYRotationDeadZone) < configs.yRotationDeadZoneAngle)
            transform.RotateAround(lockOnController.target.position, camController.transform.right, input.y * GetLockOnRotationSpeed() * Time.deltaTime);
        else
            HandleXZLockedOnMovement(new Vector2(input.y, 0));
    }

    #endregion

    private void SetModelLookAtTarget(Vector3 target) => playerModel.LookAt(target);
    
    // TODO: remove duplicated code (this function is also in the cam controller)
    private float GetAngleToHorizonPlane()
    {
        float angleToHorizonPlane = Vector3.Angle(playerModel.forward, new Vector3(playerModel.forward.x, 0, playerModel.forward.z));
        return playerModel.forward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
    }
    
    public void SnapPlayerToHorizonPlane() => playerModel.LookAt(transform.position + transform.forward);

    private float GetLockOnRotationSpeed()
    {
        Vector3 center = lockOnController.target.position;
        float circumference = (center - transform.position).magnitude * Mathf.PI * 2;
        
        return (configs.speed / circumference) * 360;
    }

    private void OnLockOn()
    {
        SnapPlayerToHorizonPlane();
        playerModel.LookAt(lockOnController.target.position);
    }

    private void OnDestroy() => lockOnController.onLockOn -= OnLockOn;
}

public enum MovementStyle
{
    DedicatedVertical,
    LockedToCamPlane,
}