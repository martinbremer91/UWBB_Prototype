using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UWBB.Components;

namespace UWBB.Systems
{
    [UpdateAfter(typeof(PlayerTranslationSystem))]
    public partial struct PlayerCharacterRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerCharacterComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Entity playerCharacterEntity = SystemAPI.GetSingletonEntity<PlayerCharacterComponent>();

            RefRO<PlayerCharacterComponent> pc = SystemAPI.GetComponentRO<PlayerCharacterComponent>(playerCharacterEntity);
            RefRW<LocalTransform> pcTransform = SystemAPI.GetComponentRW<LocalTransform>(playerCharacterEntity);

            if (pc.ValueRO.translationDirection.Equals(float3.zero))
                return;
            
            pcTransform.ValueRW.Rotation = quaternion.LookRotationSafe(pc.ValueRO.translationDirection, math.up());
        }
    }
}