using Unity.Entities;

namespace ECS_Test_Scripts.ComponentsAndTags
{
    public readonly partial struct SpawnerAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRO<SpawnerProperties> spawnerProperties;
        private readonly RefRW<SpawnerRandom> spawnerRandom;

        public int numberOfPrefabsToSpawn => spawnerProperties.ValueRO.numberOfObjectsToSpawn;
        public Entity prefabToSpawn => spawnerProperties.ValueRO.prefab;
    }
}