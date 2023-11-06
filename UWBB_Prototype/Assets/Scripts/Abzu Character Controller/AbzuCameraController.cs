using UnityEngine;

namespace DefaultNamespace.Abzu_Character_Controller
{
    public class AbzuCameraController : MonoBehaviour
    {
        [SerializeField] private AbzuInputController inputController;
        [SerializeField] private Transform player;

        [SerializeField] private float rotationSpeed = 180;

        // private void LateUpdate() => HandleCamInput(inputController.inputState.cameraAngleInput);

        private void HandleCamInput(Vector2 input)
        {
            transform.RotateAround(player.position, Vector3.up, input.x * (rotationSpeed * Time.deltaTime));

            if (Mathf.Abs(input.y) >= .75f && Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * 10) < 88)
                transform.RotateAround(player.position, transform.right, input.y * (rotationSpeed * Time.deltaTime));
        }

        private float GetAngleToHorizonPlane()
        {
            float angleToHorizonPlane = Vector3.Angle(transform.forward, new Vector3(transform.forward.x, 0, transform.forward.z));
            return transform.forward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
        }
    }
}