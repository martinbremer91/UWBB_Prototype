using ECS;
using Unity.Burst;
using Unity.Collections;
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
            RefRO<LocalTransform> cameraTransform =
                SystemAPI.GetComponentRO<LocalTransform>(SystemAPI.GetSingletonEntity<PlayerCameraTagComponent>());
            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTagComponent>();

            CharacterControllerConfigsComponent ccConfigs =
                SystemAPI.GetSingleton<CharacterControllerConfigsComponent>();

            float3 finalMovementVector = new float3();
            
            foreach (RefRO<PlayerInputComponent> inputState in SystemAPI.Query<RefRO<PlayerInputComponent>>())
            {
                float3 cameraPlaneMovementVector = 
                    GetVectorInRelationToCamRotation(cameraTransform, inputState.ValueRO.characterPlaneInput);
                finalMovementVector =
                    AddWorldYInputVectorToCameraPlaneMovementVector(inputState, cameraPlaneMovementVector);
            }

            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (RefRW<LocalTransform> playerTransform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTagComponent>())
            {
                ecb.SetComponent(playerEntity, playerTransform.ValueRW.Translate(finalMovementVector *
                    (ccConfigs.speed * SystemAPI.Time.DeltaTime)));
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
        
        private float3 GetVectorInRelationToCamRotation(RefRO<LocalTransform> cameraTransform, float2 vector)
            => math.mul(cameraTransform.ValueRO.Rotation, math.right()) * vector.x +
               math.mul(cameraTransform.ValueRO.Rotation, math.forward()) * vector.y;
        
        private float3 AddWorldYInputVectorToCameraPlaneMovementVector(RefRO<PlayerInputComponent> inputState, float3 camPlaneMove)
            => inputState.ValueRO.worldYInput == 0 ? camPlaneMove : math.up() * inputState.ValueRO.worldYInput;
    }
}