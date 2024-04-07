using ECS;
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
            state.RequireForUpdate<PlayerCharacterModelComponent>();
            state.RequireForUpdate<CharacterControllerConfigsComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Entity playerCharacterEntity = SystemAPI.GetSingletonEntity<PlayerCharacterComponent>();

            RefRO<PlayerCharacterComponent> pc = SystemAPI.GetComponentRO<PlayerCharacterComponent>(playerCharacterEntity);
            RefRW<LocalTransform> pcTransform = SystemAPI.GetComponentRW<LocalTransform>(playerCharacterEntity);

            CharacterControllerConfigsComponent ccConfigs = SystemAPI.GetSingleton<CharacterControllerConfigsComponent>();
            RefRW<LocalTransform> modelTransform =
                SystemAPI.GetComponentRW<LocalTransform>(SystemAPI.GetSingletonEntity<PlayerCharacterModelComponent>());

            bool activeTranslation = !pc.ValueRO.translationDirection.Equals(float3.zero);

            float3 lookDirection = activeTranslation ? 
                pc.ValueRO.translationDirection : GetLookDirectionLevelWithHorizon(pcTransform.ValueRO);
            
            pcTransform.ValueRW.Rotation = quaternion.LookRotationSafe(lookDirection, math.up());

            float maxAngle = ccConfigs.rotationSpeed;
            float targetAngle = modelTransform.ValueRO.Rotation.Angle(pcTransform.ValueRO.Rotation);

            float t = math.min(maxAngle / targetAngle, 1);
            modelTransform.ValueRW.Rotation =
                math.slerp(modelTransform.ValueRO.Rotation, pcTransform.ValueRO.Rotation, t);
        }
        
        private float3 GetLookDirectionLevelWithHorizon(LocalTransform transform)
        {
            float3 lookDirection = transform.Forward();
            lookDirection.y = 0;
            return lookDirection;
        }
    }
}