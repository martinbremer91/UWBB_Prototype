using Unity.Entities;
using Unity.Mathematics;

namespace UWBB.Components
{
    public struct PlayerInputComponent : IComponentData
    {
        public float2 characterPlaneInput;
        public int worldYInput;
        public float2 characterAxisInput;

        public bool dashCommand;
        public bool snapCommand;
        public bool lockOnCommand;
    }
}