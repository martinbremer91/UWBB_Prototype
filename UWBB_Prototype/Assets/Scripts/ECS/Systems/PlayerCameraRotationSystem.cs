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
            RefRW<PlayerCameraComponent> camera = SystemAPI.GetSingletonRW<PlayerCameraComponent>();

            Entity camTargetEntity = SystemAPI.GetSingletonEntity<PlayerCameraTargetComponent>();
            RefRW<LocalTransform> cameraTargetTransform =
                SystemAPI.GetComponentRW<LocalTransform>(camTargetEntity);
            RefRO<LocalToWorld> camLocalToWorld = SystemAPI.GetComponentRO<LocalToWorld>(camTargetEntity);

            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTagComponent>();
            RefRO<PlayerInputComponent> inputState = SystemAPI.GetComponentRO<PlayerInputComponent>(playerEntity);

            CharacterControllerConfigsComponent ccConfigs =
                SystemAPI.GetSingleton<CharacterControllerConfigsComponent>();

            float multipliers = ccConfigs.cameraRotationSpeed * SystemAPI.Time.DeltaTime;

            quaternion finalRotation = camera.ValueRO.mode switch
            {
                PlayerCameraMode.Free
                    => GetFreeInputModeRotation(multipliers, inputState, camLocalToWorld, cameraTargetTransform),
                PlayerCameraMode.Reset
                    => GetResetCameraModeRotation(ref state, camera, cameraTargetTransform.ValueRO.Rotation,
                        ccConfigs.cameraSmoothingSpeed),
                PlayerCameraMode.SnapToHorizon
                    // TEMP => GetSnapCameraModeRotation(),
                    => GetResetCameraModeRotation(ref state, camera, cameraTargetTransform.ValueRO.Rotation,
                        ccConfigs.cameraSmoothingSpeed),
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

        private quaternion GetResetCameraModeRotation(ref SystemState state, RefRW<PlayerCameraComponent> cam,
            quaternion camRotation, float smoothingSpeed)
        {
            if (cam.ValueRO.smoothingDuration == 0)
            {
                Entity playerCharacterEntity = SystemAPI.GetSingletonEntity<PlayerCharacterComponent>();
                RefRO<LocalTransform> characterTransform = SystemAPI.GetComponentRO<LocalTransform>(playerCharacterEntity);
                
                cam.ValueRW.smoothingDuration =
                    GetSmoothingDuration(camRotation, characterTransform.ValueRO.Rotation, smoothingSpeed);
                cam.ValueRW.targetRotation = characterTransform.ValueRO.Rotation;
            }
            else
                cam.ValueRW.smoothingTimer += SystemAPI.Time.DeltaTime;

            float t = math.min(1, cam.ValueRO.smoothingTimer / cam.ValueRO.smoothingDuration);

            if (cam.ValueRO.smoothingTimer >= cam.ValueRO.smoothingDuration)
                cam.ValueRW.mode = PlayerCameraMode.Free;

            return math.slerp(camRotation, cam.ValueRW.targetRotation, t / 10);
        }

        private quaternion GetSnapCameraModeRotation()
        {
            return default;
        }

        private float GetSmoothingDuration(quaternion camRotation, quaternion characterRotation, float smoothingSpeed)
            => camRotation.Angle(characterRotation) / smoothingSpeed;
    }
}