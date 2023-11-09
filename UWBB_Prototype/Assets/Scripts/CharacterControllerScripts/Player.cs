using UWBB.GameFramework;
using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        private PlayerLogic<IInputState, IInputState> inputLogic;
        private PlayerLogic<IInputState, IMovementLogicData> movementLogic;
        private PlayerLogic<IInputState, ICameraLogicData> cameraLogic;
        private PlayerLogic<IInputState, ILockOnLogicData> lockOnLogic;
        
        private readonly MovementController movementController = new();
        private readonly CameraController cameraController = new();
        private readonly LockOnController lockOnController = new();

        private CharacterControllerConfigs configs;
        
        private CharacterControllerConfigs.MovementLogicType movementLogicType;
        private CharacterControllerConfigs.LockOnLogicType lockOnLogicType;
        
        public void Init() => configs = Main.instance.configs.ccConfigs;

        private void Update()
        {
            IInputState inputState = inputLogic.RunUpdate(null);
        }

        private void LateUpdate()
        {
            // camera stuff goes here?
        }
    }
}