using System;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Camera : ICharacterController
    {
        private Transform pivot;
        private Transform cameraTransform;

        private const float camSpeed = 10;
        
        public void Init(ICharacter character)
        {
            if (Camera.main != null)
                cameraTransform = Camera.main.transform;
            else
                throw new NullReferenceException("CameraController could not find object with MainCamera tag");
            
            pivot = character.monoBehaviour.transform;
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