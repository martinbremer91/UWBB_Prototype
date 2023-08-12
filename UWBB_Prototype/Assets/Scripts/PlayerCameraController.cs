using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    [SerializeField] private Transform player;
    private Transform camHolder;

    [SerializeField] private float rotationSpeed = 180;

    private void Awake() => camHolder = transform.parent;

    private void Update()
    {
        // HandleYInput(inputController.inputState.yInput);
    }

    private void HandleYInput(Vector2 input)
    {
        camHolder.Rotate(new Vector3(0, input.x, 0) * (rotationSpeed * Time.deltaTime), Space.Self);
    }

    public Vector3 GetVectorInRelationToCamRotation(Vector2 vector)
    {
        var tf = transform;
        return tf.right * vector.x + tf.forward * vector.y;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 inputVector = inputController.inputState.xzInput;
        
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
