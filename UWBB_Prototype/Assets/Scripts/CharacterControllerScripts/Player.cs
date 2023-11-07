using MovementControllerType = UWBB.CharacterController.CharacterControllerConfigs.MovementControllerType;
using LockOnControllerType = UWBB.CharacterController.CharacterControllerConfigs.LockOnControllerType;
using UWBB.GameFramework;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        public MovementController movementController = new();
        public CameraController cameraController = new();
        public LockOnController lockOnController = new();

        private CharacterControllerConfigs configs;
        
        private MovementControllerType movementControllerType;
        private LockOnControllerType lockOnControllerType;
        
        public void Init() => configs = Main.instance.configs.ccConfigs;

        private void Update()
        {
            ResolveActiveControllerTypes();
        }

        private void ResolveActiveControllerTypes()
        {
            MovementControllerType activeMovement = configs.activeMovementController;
            LockOnControllerType activeLockOn = configs.activeLockOnController;

            if (activeMovement != movementControllerType)
                SetActiveMovementController(activeMovement);
            if (activeLockOn != lockOnControllerType)
                SetActiveLockOnController(activeLockOn);
        }

        private void SetActiveMovementController(MovementControllerType activeMovement)
        {
            
        }
        
        private void SetActiveLockOnController(LockOnControllerType activeLockOn)
        {
            
        }
    }
}