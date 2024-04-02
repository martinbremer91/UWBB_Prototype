using ECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UWBB.Components;

namespace UWBB.Systems
{
    public partial struct PlayerTranslationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTagComponent>();
            state.RequireForUpdate<PlayerInputComponent>();
            state.RequireForUpdate<PlayerCameraTagComponent>();
            state.RequireForUpdate<CharacterControllerConfigsComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTagComponent>();
            RefRW<LocalTransform> playerTransform = SystemAPI.GetComponentRW<LocalTransform>(playerEntity);
            RefRO<PlayerInputComponent> inputState = SystemAPI.GetComponentRO<PlayerInputComponent>(playerEntity);

            CharacterControllerConfigsComponent ccConfigs =
                SystemAPI.GetSingleton<CharacterControllerConfigsComponent>();
            
            RefRO<LocalToWorld> cameraLocalToWorld =
                SystemAPI.GetComponentRO<LocalToWorld>(SystemAPI.GetSingletonEntity<PlayerCameraTagComponent>());
            
            float inputX = inputState.ValueRO.characterPlaneInput.x;
            float inputY = inputState.ValueRO.characterPlaneInput.y;

            float3 camForwardWorld = cameraLocalToWorld.ValueRO.Value.TransformDirection(new float3(0, 0, 1));
            float3 camRightWorld = cameraLocalToWorld.ValueRO.Value.TransformDirection(new float3(1, 0, 0));

            float3 finalMovementVector = math.normalizesafe(camForwardWorld * inputY + camRightWorld * inputX);
            
            playerTransform.ValueRW.Position = 
                playerTransform.ValueRW.Translate(finalMovementVector * (ccConfigs.speed * SystemAPI.Time.DeltaTime)).Position;
        }
    }
}