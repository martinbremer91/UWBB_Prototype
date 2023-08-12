using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    [SerializeField] private PlayerCameraController camController;
    [SerializeField] private Transform playerModel;
    
    public float speed = 5;

    private void Update()
    {
        HandleXZMovement(inputController.inputState.xzInput);
        HandleYMovementAndRotation(inputController.inputState.yInput);
    }
    
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
}
