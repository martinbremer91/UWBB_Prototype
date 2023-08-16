using ECS_Test_Scripts.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

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
            
            var prefabOffset = new float3(0f, -2f, 1f);
            
            var builder = new BlobBuilder(Allocator.Temp);
            ref var spawnPoints = ref builder.ConstructRoot<PrefabSpawnPointsBlob>();
            var arrayBuilder = builder.Allocate(ref spawnPoints.Value, spawner.numberOfPrefabsToSpawn);

            for (int i = 0; i < spawner.numberOfPrefabsToSpawn; i++)
            {
                var instantiatedEntity = ecb.Instantiate(spawner.prefabToSpawn);
                var randomTransform = spawner.GetRandomPrefabTransform();
                ecb.SetComponent(instantiatedEntity, randomTransform);
                
                var newPrefabSpawnPoint = randomTransform.Position + prefabOffset;
                arrayBuilder[i] = newPrefabSpawnPoint;
            }

            var blobAsset = builder.CreateBlobAssetReference<PrefabSpawnPointsBlob>(Allocator.Persistent);
            ecb.SetComponent(spawnerEntity, new PrefabSpawnPoints {Value = blobAsset});
            builder.Dispose();
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}
