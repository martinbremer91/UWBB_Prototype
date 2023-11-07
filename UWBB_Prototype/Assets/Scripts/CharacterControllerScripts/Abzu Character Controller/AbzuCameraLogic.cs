using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuCameraLogic : ICameraLogic
    {
        private AbzuInputLogic inputLogic;
        private Transform player;

        private float rotationSpeed = 180;

        // private void LateUpdate() => HandleCamInput(inputController.inputState.cameraAngleInput);

        private void HandleCamInput(Vector2 input)
        {
            // transform.RotateAround(player.position, Vector3.up, input.x * (rotationSpeed * Time.deltaTime));
            //
            // if (Mathf.Abs(input.y) >= .75f && Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * 10) < 88)
            //     transform.RotateAround(player.position, transform.right, input.y * (rotationSpeed * Time.deltaTime));
        }

        private float GetAngleToHorizonPlane()
        {
            // var forward = transform.forward;
            // float angleToHorizonPlane = Vector3.Angle(forward, new Vector3(forward.x, 0, forward.z));
            // return forward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
            return 0;
        }
    }
}