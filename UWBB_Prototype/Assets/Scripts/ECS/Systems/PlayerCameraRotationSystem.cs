using System;
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
            state.RequireForUpdate<PlayerCharacterComponent>();
            state.RequireForUpdate<PlayerCameraComponent>();
            state.RequireForUpdate<PlayerCameraTargetComponent>();
            state.RequireForUpdate<CharacterControllerConfigsComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            PlayerCameraComponent camera = SystemAPI.GetSingleton<PlayerCameraComponent>();
            
            Entity camTargetEntity = SystemAPI.GetSingletonEntity<PlayerCameraTargetComponent>();
            RefRW<LocalTransform> cameraTargetTransform =
                SystemAPI.GetComponentRW<LocalTransform>(camTargetEntity);
            RefRO<LocalToWorld> camLocalToWorld = SystemAPI.GetComponentRO<LocalToWorld>(camTargetEntity);

            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTagComponent>();
            RefRO<PlayerInputComponent> inputState = SystemAPI.GetComponentRO<PlayerInputComponent>(playerEntity);

            CharacterControllerConfigsComponent ccConfigs =
                SystemAPI.GetSingleton<CharacterControllerConfigsComponent>();

            float multipliers = ccConfigs.cameraRotationSpeed * SystemAPI.Time.DeltaTime;
            
            quaternion finalRotation = camera.mode switch 
            {
                PlayerCameraMode.Free 
                    => GetFreeInputModeRotation(multipliers, inputState, camLocalToWorld, cameraTargetTransform),
                PlayerCameraMode.Reset 
                    => GetResetCameraModeRotation(ref state),
                PlayerCameraMode.SnapToHorizon 
                    => GetSnapCameraModeRotation(),
                _ => throw new ArgumentOutOfRangeException()
            };

            cameraTargetTransform.ValueRW.Rotation = finalRotation;
        }

        private quaternion GetFreeInputModeRotation(
            float multipliers,
            RefRO<PlayerInputComponent> inputState, 
            RefRO<LocalToWorld> camLocalToWorld, 
            RefRW<LocalTransform> cameraTargetTransform)
        {
            float angleVertical = inputState.ValueRO.characterAxisInput.y * multipliers;
            float angleHorizontal = inputState.ValueRO.characterAxisInput.x * multipliers;
            
            float3 worldUpInLocal = camLocalToWorld.ValueRO.Value.InverseTransformDirection(new float3(0, 1, 0));
            
            quaternion currentRotation = cameraTargetTransform.ValueRW.Rotation;
            quaternion targetYRotation = math.mul(math.normalizesafe(currentRotation),
                quaternion.AxisAngle(worldUpInLocal, angleHorizontal));
            
            return math.mul(targetYRotation, quaternion.AxisAngle(math.right(), angleVertical));
        }

        private quaternion GetResetCameraModeRotation(ref SystemState state)
        {
            Entity playerCharacterEntity = SystemAPI.GetSingletonEntity<PlayerCharacterComponent>();
            RefRO<LocalTransform> characterTransform = SystemAPI.GetComponentRO<LocalTransform>(playerCharacterEntity);
            return characterTransform.ValueRO.Rotation;
        }

        private quaternion GetSnapCameraModeRotation()
        {
            return default;
        }
    }
}