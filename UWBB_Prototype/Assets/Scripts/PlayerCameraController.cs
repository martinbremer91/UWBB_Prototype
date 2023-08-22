using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    [SerializeField] private PlayerLockOnController lockOnController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform player;

    [SerializeField] private float rotationSpeed = 180;

    private void Awake() => lockOnController.onLockOn += OnLockOn;

    private void LateUpdate()
    {
        if (inputController.inputState.snapCommand)
        {
            SnapCamToHorizonPlane();
            inputController.inputState.snapCommand = false;
        } else if (!lockOnController.lockedOn)
            HandleCamInput(inputController.inputState.characterAxisInput);
        else
            OnLockOn();
    }

    private void HandleCamInput(Vector2 input)
    {
        transform.RotateAround(player.position, Vector3.up, input.x * (rotationSpeed * Time.deltaTime));

        if (Mathf.Abs(input.y) >= .75f && Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * 10) < 88)
            transform.RotateAround(player.position, transform.right, input.y * (rotationSpeed * Time.deltaTime));
    }

    public Vector3 GetVectorInRelationToCamRotation(Vector2 vector)
    {
        var tf = transform;
        return tf.right * vector.x + tf.forward * vector.y;
    }

    private void SnapCamToHorizonPlane()
    {
        float angleToHorizonPlane = GetAngleToHorizonPlane();
        transform.RotateAround(player.position, transform.right, angleToHorizonPlane);
        playerMovement.SnapPlayerToHorizonPlane();
    }
    
    private float GetAngleToHorizonPlane()
    {
        float angleToHorizonPlane = Vector3.Angle(transform.forward, new Vector3(transform.forward.x, 0, transform.forward.z));
        return transform.forward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
    }

    private void OnLockOn()
    {
        Vector3 targetPlayerDirection = (player.position - lockOnController.target.position).normalized;
        float distanceToPlayer = (player.position - transform.position).magnitude;
        transform.position = player.position + targetPlayerDirection * distanceToPlayer;
        transform.LookAt(lockOnController.target.position);
    }

    private void OnDestroy()
    {
        lockOnController.onLockOn -= OnLockOn;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 inputVector = inputController.inputState.characterPlaneInput;
        
        var playerPosition = player.position;
        var tf = transform;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerPosition - tf.right + Vector3.down, playerPosition + tf.right + Vector3.down);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerPosition - tf.forward + Vector3.down, playerPosition + tf.forward + Vector3.down);
        
        if (inputVector.magnitude == 0)
            return;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(playerPosition, playerPosition + tf.right * inputVector.x + tf.forward * inputVector.y);
    }
#endif
}