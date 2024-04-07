using Unity.Entities;
using Unity.Mathematics;

namespace UWBB.Components
{
    public struct PlayerCharacterComponent : IComponentData
    {
        public float3 translationDirection;
    }
}