using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class CameraController : IPlayerController
    {
        public IMovementLogic movementLogic { get; set; }
        public ICameraLogic cameraLogic { get; set; }
        public ILockOnLogic lockOnLogic { get; set; }
    }
}
