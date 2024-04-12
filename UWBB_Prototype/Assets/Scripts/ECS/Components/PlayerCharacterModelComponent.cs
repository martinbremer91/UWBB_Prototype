using Unity.Entities;
using Unity.Mathematics;

namespace UWBB.Components
{
    public struct PlayerCharacterModelComponent : IComponentData
    {
        public float3 translationDirection;
    }
}