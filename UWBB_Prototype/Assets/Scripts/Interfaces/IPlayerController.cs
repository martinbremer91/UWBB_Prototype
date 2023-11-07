using MovementLogicType = UWBB.CharacterController.CharacterControllerConfigs.MovementLogicType;
using LockOnLogicType = UWBB.CharacterController.CharacterControllerConfigs.LockOnLogicType;
using UWBB.CharacterController.Abzu;
using UWBB.CharacterController.FirstVersion;

namespace UWBB.Interfaces
{
    public interface IPlayerController
    {
        public IMovementLogic movementLogic { get; set; }
        public ICameraLogic cameraLogic { get; set; }
        public ILockOnLogic lockOnLogic { get; set; }

        public void SetActiveMovementLogic(MovementLogicType movementLogicType)
        {
            movementLogic = movementLogicType switch
            {
                MovementLogicType.Default => new FirstVersionMovementLogic(),
                MovementLogicType.Abzu => new AbzuMovementLogic(),
            };

            cameraLogic = movementLogicType switch
            {
                MovementLogicType.Default => new FirstVersionCameraLogic(),
                MovementLogicType.Abzu => new AbzuCameraLogic(),
            };
        }
        
        public void SetActiveLockOnLogic(LockOnLogicType lockOnLogicType)
        {
            lockOnLogic = lockOnLogicType switch
            {
                LockOnLogicType.Default => new FirstVersionLockOnLogic(),
                LockOnLogicType.Abzu => new AbzuLockOnLogic(),
            };
        }
    }
}