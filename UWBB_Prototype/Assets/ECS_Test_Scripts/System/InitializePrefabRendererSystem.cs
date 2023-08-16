using ECS_Test_Scripts.AuthoringAndMono;
using ECS_Test_Scripts.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECS_Test_Scripts.System
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(SpawnPrefabSystem))]
    public partial struct InitializeTombstoneRendererSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnerProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            ecb.Playback(state.EntityManager);
        }
    }
}