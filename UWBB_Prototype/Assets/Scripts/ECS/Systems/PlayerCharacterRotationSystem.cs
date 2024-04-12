using ECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
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
            state.RequireForUpdate<PlayerCharacterModelComponent>();
            state.RequireForUpdate<CharacterControllerConfigsComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Entity playerCharacterEntity = SystemAPI.GetSingletonEntity<PlayerCharacterModelComponent>();

            RefRO<PlayerCharacterModelComponent> pc = SystemAPI.GetComponentRO<PlayerCharacterModelComponent>(playerCharacterEntity);
            RefRW<LocalTransform> pcTransform = SystemAPI.GetComponentRW<LocalTransform>(playerCharacterEntity);

            CharacterControllerConfigsComponent ccConfigs = SystemAPI.GetSingleton<CharacterControllerConfigsComponent>();
            RefRW<LocalTransform> modelTransform =
                SystemAPI.GetComponentRW<LocalTransform>(SystemAPI.GetSingletonEntity<PlayerCharacterModelComponent>());

            Entity pcmEntity = SystemAPI.GetSingletonEntity<PlayerCharacterModelComponent>();
            RefRW<PhysicsVelocity> playerVelocity = SystemAPI.GetComponentRW<PhysicsVelocity>(pcmEntity);
            
            bool activeTranslation = !pc.ValueRO.translationDirection.Equals(float3.zero);
            float3 lookDirection = activeTranslation ? 
                pc.ValueRO.translationDirection : GetLookDirectionLevelWithHorizon(pcTransform.ValueRO);
        }
        
        private float3 GetLookDirectionLevelWithHorizon(LocalTransform transform)
        {
            float3 lookDirection = transform.Forward();
            lookDirection.y = 0;
            return lookDirection;
        }
    }
}