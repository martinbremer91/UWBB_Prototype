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

            float deltaTime = SystemAPI.Time.DeltaTime;
            float angleX = inputState.ValueRO.characterAxisInput.y * deltaTime;
            float angleY = inputState.ValueRO.characterAxisInput.x * deltaTime;
            
            float3 worldUpInLocal = camLocalToWorld.ValueRO.Value.InverseTransformDirection(new float3(0, 1, 0));
            
            quaternion currentRotation = cameraTargetTransform.ValueRW.Rotation;
            quaternion targetYRotation = math.mul(math.normalizesafe(currentRotation), quaternion.AxisAngle(worldUpInLocal, angleY));
            quaternion finalRotation = math.mul(targetYRotation, quaternion.AxisAngle(math.right(), angleX));

            cameraTargetTransform.ValueRW.Rotation = finalRotation;
        }
    }
}