using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Camera
    {
        private readonly Transform pivot;
        private readonly Transform cameraTransform;

        private const float camSpeed = 10;
        
        public CharacterController_Camera(Transform cameraTransform, Transform pivot)
        {
            this.cameraTransform = cameraTransform;
            this.pivot = pivot;
        }
        
        public void Update(InputState input)
        {
            Vector2 scaledInput = input.cameraAim * (Time.deltaTime * camSpeed);
            float xAxisRotation = scaledInput.x;
            float yAxisRotation = scaledInput.y;

            Vector3 camLevelledForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
            float yAxisAngle = Vector3.Angle(camLevelledForward, cameraTransform.forward);

            if (yAxisAngle + Mathf.Abs(yAxisRotation) > 70)
            {
                if (cameraTransform.forward.y > 0 && yAxisRotation < 0 ||
                    cameraTransform.forward.y < 0 && yAxisRotation > 0)
                    yAxisRotation = 0;
            }
            
            cameraTransform.RotateAround(pivot.position, Vector3.up, xAxisRotation);
            cameraTransform.RotateAround(pivot.position, cameraTransform.right, yAxisRotation);
        }
    }
}