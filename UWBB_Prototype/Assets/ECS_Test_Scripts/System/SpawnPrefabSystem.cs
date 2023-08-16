using ECS_Test_Scripts.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECS_Test_Scripts.System
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnPrefabSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnerProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            
            var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerProperties>();
            var spawner = SystemAPI.GetAspect<SpawnerAspect>(spawnerEntity);
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            for (int i = 0; i < spawner.numberOfPrefabsToSpawn; i++)
            {
                var instantiatedEntity = ecb.Instantiate(spawner.prefabToSpawn);
                var randomTransform = spawner.GetRandomPrefabTransform();
                ecb.SetComponent(instantiatedEntity, randomTransform);
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}
