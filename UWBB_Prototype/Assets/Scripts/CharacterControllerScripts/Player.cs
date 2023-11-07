using UWBB.GameFramework;
using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        private readonly MovementController movementController = new();
        private readonly CameraController cameraController = new();
        private readonly LockOnController lockOnController = new();

        private CharacterControllerConfigs configs;
        
        private CharacterControllerConfigs.MovementLogicType movementLogicType;
        private CharacterControllerConfigs.LockOnLogicType lockOnLogicType;
        
        public void Init() => configs = Main.instance.configs.ccConfigs;

        private void Update()
        {
            ResolveActiveControllerTypes();
        }

        private void ResolveActiveControllerTypes()
        {
            CharacterControllerConfigs.MovementLogicType activeMovement = configs.activeMovementLogic;
            CharacterControllerConfigs.LockOnLogicType activeLockOn = configs.activeLockOnLogic;

            if (activeMovement != movementLogicType)
                SetActiveMovementLogic(activeMovement);
            if (activeLockOn != lockOnLogicType)
                SetActiveLockOnLogic(activeLockOn);
        }

        private void SetActiveMovementLogic(CharacterControllerConfigs.MovementLogicType activeMovement)
        {
            (movementController as IPlayerController).SetActiveMovementLogic(activeMovement);
            (cameraController as IPlayerController).SetActiveMovementLogic(activeMovement);
            (lockOnController as IPlayerController).SetActiveMovementLogic(activeMovement);
        }
        
        private void SetActiveLockOnLogic(CharacterControllerConfigs.LockOnLogicType activeLockOn)
        {
            (movementController as IPlayerController).SetActiveLockOnLogic(activeLockOn);
            (cameraController as IPlayerController).SetActiveLockOnLogic(activeLockOn);
            (lockOnController as IPlayerController).SetActiveLockOnLogic(activeLockOn);
        }
    }
}