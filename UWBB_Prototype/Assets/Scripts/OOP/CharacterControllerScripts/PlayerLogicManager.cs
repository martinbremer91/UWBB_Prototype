using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class PlayerLogicManager
    {
        private Player player;
        private LockOnController lockOnController;

        private PlayerInputLogic inputLogic;
        private PlayerMovementLogic movementLogic;
        private PlayerCameraLogic cameraLogic;
        private PlayerLockOnLogic lockOnLogic;
        
        public void Init(Player p)
        {
            player = p;
            lockOnController = p.lockOnController;

            inputLogic = new PlayerInputLogic();
            inputLogic.Init();
            movementLogic = new PlayerMovementLogic();
            movementLogic.Init(p);
            cameraLogic = new PlayerCameraLogic();
            cameraLogic.Init(p);
            lockOnLogic = new PlayerLockOnLogic();
            lockOnLogic.Init(p);
        }
        
        public void RunLogicUpdate()
        {
            InputState inputState = inputLogic.inputState;
            
            lockOnController.CompleteValidationJobs();
            PlayerLockOnData lockOnData = lockOnLogic.RunUpdate(inputState);
            lockOnController.ApplyLockOnLogicData(lockOnData);
            MBre.Utilities.DebugPanel.CustomDebug("LockedOn: " + lockOnController.lockedOn + 
                                                  " // Target: " + lockOnController.activeTarget?.lockTarget.name);
            lockOnController.DisposeNativeArrays();
            
            player.movementData = movementLogic.RunUpdate(inputState);
            player.cameraData = cameraLogic.RunUpdate(inputState);
        }
    }
}