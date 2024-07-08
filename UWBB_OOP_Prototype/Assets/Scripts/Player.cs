using UnityEngine;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform modelTransform;
        [SerializeField] private Transform cameraTransform;
        
        private InputState inputState = new();
        private CharacterController_Input inputController;
        private CharacterController_Movement movementController;
        private CharacterController_Camera characterControllerCamera;

        private void Awake()
        {
            inputController = new CharacterController_Input();
            movementController = new CharacterController_Movement(transform, modelTransform, cameraTransform);
            characterControllerCamera = new CharacterController_Camera(cameraTransform, transform);
        }

        private void Update()
        {
            inputController.Update(inputState);
            movementController.Update(inputState);
        }

        private void LateUpdate()
        { 
            characterControllerCamera.Update(inputState);
        }

        private Vector3 test;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + test * 100);
        }
    }
}
