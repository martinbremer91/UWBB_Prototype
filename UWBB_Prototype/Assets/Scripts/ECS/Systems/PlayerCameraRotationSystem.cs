using ECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UWBB.Components;

namespace UWBB.Systems
{
    public partial struct PlayerCameraRotationSystem : ISystem
    { 
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTagComponent>();
            state.RequireForUpdate<PlayerCameraTagComponent>();
            state.RequireForUpdate<PlayerCameraTargetTagComponent>();
            state.RequireForUpdate<CharacterControllerConfigsComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Entity camTargetEntity = SystemAPI.GetSingletonEntity<PlayerCameraTargetTagComponent>();
            RefRW<LocalTransform> cameraTargetTransform =
                SystemAPI.GetComponentRW<LocalTransform>(camTargetEntity);
            RefRO<LocalToWorld> camLocalToWorld = SystemAPI.GetComponentRO<LocalToWorld>(camTargetEntity);

            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTagComponent>();
            RefRO<PlayerInputComponent> inputState = SystemAPI.GetComponentRO<PlayerInputComponent>(playerEntity);

            CharacterControllerConfigsComponent ccConfigs =
                SystemAPI.GetSingleton<CharacterControllerConfigsComponent>();
            
            float multipliers = SystemAPI.Time.DeltaTime * ccConfigs.cameraRotationSpeed;
            float angleVertical = inputState.ValueRO.characterAxisInput.y * multipliers;
            float angleHorizontal = inputState.ValueRO.characterAxisInput.x * multipliers;
            
            float3 worldUpInLocal = camLocalToWorld.ValueRO.Value.InverseTransformDirection(new float3(0, 1, 0));
            
            quaternion currentRotation = cameraTargetTransform.ValueRW.Rotation;
            quaternion targetYRotation = math.mul(math.normalizesafe(currentRotation),
                quaternion.AxisAngle(worldUpInLocal, angleHorizontal));
            quaternion finalRotation = math.mul(targetYRotation, quaternion.AxisAngle(math.right(), angleVertical));

            cameraTargetTransform.ValueRW.Rotation = finalRotation;
        }
    }
}