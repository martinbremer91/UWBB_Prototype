using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace BoidsExperiment
{
    [BurstCompile]
    public partial struct SpawnBoidsSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BoidSpawner>();
            state.RequireForUpdate<BoidVolumeAndPartitions>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            BoidSpawner spawner = SystemAPI.GetSingleton<BoidSpawner>();
            BoidVolumeAndPartitions partitionComponent = SystemAPI.GetSingleton<BoidVolumeAndPartitions>();

            state.EntityManager.Instantiate(spawner.boidPrefab, spawner.numberOfBoids, Allocator.Temp);

            uint seed = (uint)SystemAPI.Time.ElapsedTime;
            var random = Random.CreateFromIndex(seed);

            float3 centerAlignmentOffset = new float3(.5f, .5f, .5f);
                
            foreach (var (transform, direction) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BoidDirection>>().WithAll<BoidSpeed>())
            {
                float3 offsetFromCenter = (random.NextFloat3() - centerAlignmentOffset) * partitionComponent.volume;
                transform.ValueRW.Position = partitionComponent.center + offsetFromCenter;

                direction.ValueRW.direction = random.NextFloat3(new float3(-1, -1, -1), new float3(1, 1, 1));
            }
        }
    }
}