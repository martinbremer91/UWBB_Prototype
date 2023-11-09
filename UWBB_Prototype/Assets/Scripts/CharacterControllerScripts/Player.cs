using UWBB.GameFramework;
using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        private IInputLogic inputLogic;
        private IMovementLogic movementLogic;
        private ICameraLogic cameraLogic;
        private ILockOnLogic lockOnLogic;
        
        private readonly MovementController movementController = new();
        private readonly CameraController cameraController = new();
        private readonly LockOnController lockOnController = new();

        private CharacterControllerConfigs configs;
        
        private CharacterControllerConfigs.MovementLogicType movementLogicType;
        private CharacterControllerConfigs.LockOnLogicType lockOnLogicType;
        
        public void Init() => configs = Main.instance.configs.ccConfigs;

        private void Update()
        {
            
        }

        private void LateUpdate()
        {
            // camera stuff goes here?
        }
    }
}