using ECS_Test_Scripts.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECS_Test_Scripts.System
{
    [BurstCompile]
    public partial struct SpawnWalkerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WalkerSpawnTimer>();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state) {}

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new SpawnWalkerJob()
            {
                deltaTime = deltaTime,
                ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }

    [BurstCompile]
    public partial struct SpawnWalkerJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer ecb;
        private void Execute(SpawnerAspect spawner)
        {
            spawner.walkerSpawnTimer -= deltaTime;
            
            if (!spawner.timeToSpawnWalker)
                return;

            spawner.walkerSpawnTimer = spawner.walkerSpawnRate;
            var newWalker = ecb.Instantiate(spawner.walkerPrefab);
        }
    }
}