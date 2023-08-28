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
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            BoidSpawner spawner = SystemAPI.GetSingleton<BoidSpawner>();

            state.EntityManager.Instantiate(spawner.boidPrefab, spawner.numberOfBoids, Allocator.Temp);

            uint seed = (uint)SystemAPI.Time.ElapsedTime;
            var random = Random.CreateFromIndex(seed);

            foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<BoidSpeed>())
            {
                float3 offsetFromCenter = random.NextFloat3() * spawner.spawnerVolume;
                transform.ValueRW.Position = spawner.center + offsetFromCenter;

                float3 direction = random.NextFloat3(new float3(-1, -1, -1), new float3(1, 1, 1));
                transform.ValueRW.Rotation = quaternion.LookRotation(direction, new float3(0, 1, 0));
            }
        }
    }
}