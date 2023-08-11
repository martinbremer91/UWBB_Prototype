using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    public float speed = 5;
    public float rotationSpeed = 5;

    private void Update()
    {
        HandleXZMovement(inputController.inputState.xzInput);
        HandleYMovementAndRotation(inputController.inputState.yInput);
    }
    
    private void HandleXZMovement(Vector2 input) =>
        transform.Translate(new Vector3(input.x, 0, input.y) * (speed * Time.deltaTime), Space.Self);
    
    private void HandleYMovementAndRotation(Vector2 input)
    {
        transform.Translate(new Vector3(0, input.y, 0) * (speed * Time.deltaTime));
        transform.Rotate(new Vector3(0, input.x, 0) * (rotationSpeed * Time.deltaTime), Space.Self);
    }
}
