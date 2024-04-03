using Unity.Entities;

namespace UWBB.Components
{
    public struct PlayerCameraComponent : IComponentData
    {
        public PlayerCameraMode mode;
    }
    
    public enum PlayerCameraMode
    {
        Free = 0,
        Reset = 1 << 0,
        SnapToHorizon = 1 << 1,
    }
}