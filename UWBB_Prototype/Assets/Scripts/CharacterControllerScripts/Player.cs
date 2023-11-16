using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        private readonly PlayerLogicManager playerLogicManager = new();
        
        public IMovementLogicData movementData;
        public ICameraLogicData cameraData;
        
        private readonly MovementController movementController = new();
        private readonly CameraController cameraController = new();
        public readonly LockOnController lockOnController = new();

        public Transform playerModel;
        public Transform cameraTransform;

        public void Init()
        {
            playerLogicManager.Init(this);
            movementController.Init(this);
            cameraController.Init(this);
            lockOnController.Init(this);
        }

        private void Update()
        {
            lockOnController.ValidateTargets();
            playerLogicManager.RunLogicUpdate();
            movementController.ProcessMovementData(movementData);
        }

        private void LateUpdate() => cameraController.ProcessCameraData(cameraData);
    }
}