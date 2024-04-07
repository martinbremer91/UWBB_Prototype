using Unity.Entities;
using Unity.Mathematics;

namespace UWBB.Components
{
    public struct PlayerCameraComponent : IComponentData
    {
        public PlayerCameraMode mode;
        public float smoothingDuration;
        public float smoothingTimer;
        public quaternion targetRotation;
    }
    
    public enum PlayerCameraMode
    {
        Free = 0,
        Reset = 1 << 0,
        SnapToHorizon = 1 << 1,
    }
}