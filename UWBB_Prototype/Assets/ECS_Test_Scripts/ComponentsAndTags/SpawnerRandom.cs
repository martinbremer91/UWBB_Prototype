using Unity.Entities;
using Unity.Mathematics;

namespace ECS_Test_Scripts.ComponentsAndTags
{
    public struct SpawnerRandom : IComponentData
    {
        public Random value;
    }
}