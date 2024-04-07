using System;
using ECS;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
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
                    => GetFreeInputModeRotation(multipliers, ccConfigs.cameraClampDotProduct, inputState, 
                        camLocalToWorld, cameraTargetTransform),
                PlayerCameraMode.Reset
                    => GetResetCameraModeRotation(ref state, camera, cameraTargetTransform.ValueRO.Rotation,
                        ccConfigs.cameraSmoothingSpeed),
                PlayerCameraMode.SnapToHorizon
                    => GetSnapCameraModeRotation(ref state, camera, cameraTargetTransform.ValueRO.Rotation,
                        ccConfigs.cameraSmoothingSpeed),
                _ => throw new ArgumentOutOfRangeException()
            };

            cameraTargetTransform.ValueRW.Rotation = finalRotation;
        }

        private quaternion GetFreeInputModeRotation(
            float multipliers,
            float clampDotProduct,
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
            quaternion targetRotation = math.mul(targetYRotation, quaternion.AxisAngle(math.right(), angleVertical));
            quaternion clampedTargetRotation = ClampTargetRotation(targetYRotation, angleVertical, clampDotProduct)
                ? targetYRotation
                : targetRotation;
            
            return clampedTargetRotation;
        }

        private bool ClampTargetRotation(quaternion targetRotation, float angleVertical, float clampDotProduct)
        {
            float3 forward = math.mul(targetRotation, new float3(0, 0, 1));
            float dot = math.dot(forward, math.up());
            bool clampInput = (angleVertical > 0 && dot < 0) || (angleVertical < 0 && dot > 0);
            return math.abs(dot) > clampDotProduct && clampInput;
        }

        private quaternion GetResetCameraModeRotation(ref SystemState state, RefRW<PlayerCameraComponent> cam,
            quaternion camRotation, float smoothingSpeed)
        {
            bool incrementTimer = !SetTargetRotation_Reset(ref state, cam, camRotation, smoothingSpeed);
            return GetSmoothedRotation(ref state, incrementTimer, cam, camRotation);
        }

        private quaternion GetSnapCameraModeRotation(ref SystemState state, RefRW<PlayerCameraComponent> cam,
            quaternion camRotation, float smoothingSpeed)
        {
            bool incrementTimer = !SetTargetRotation_Snap(cam, camRotation, smoothingSpeed);
            return GetSmoothedRotation(ref state, incrementTimer, cam, camRotation);
        }
        
        private float GetSmoothingDuration(quaternion camRotation, quaternion targetRotation, float smoothingSpeed)
            => camRotation.Angle(targetRotation) / smoothingSpeed;

        private quaternion GetSmoothedRotation(ref SystemState state, bool incrementTimer,
            RefRW<PlayerCameraComponent> cam, quaternion camRotation)
        {
            if (incrementTimer)
                cam.ValueRW.smoothingTimer += SystemAPI.Time.DeltaTime;
            
            if (cam.ValueRO.smoothingTimer >= cam.ValueRO.smoothingDuration)
                cam.ValueRW.mode = PlayerCameraMode.Free;
            
            float t = math.min(1, cam.ValueRO.smoothingTimer / cam.ValueRO.smoothingDuration);
            return math.slerp(camRotation, cam.ValueRW.targetRotation, t);
        }

        private bool SetTargetRotation_Reset(ref SystemState state, RefRW<PlayerCameraComponent> cam,
            quaternion camRotation, float smoothingSpeed)
        {
            if (cam.ValueRO.smoothingDuration == 0)
            {
                Entity playerCharacterEntity = SystemAPI.GetSingletonEntity<PlayerCharacterComponent>();
                RefRO<LocalTransform> characterTransform = SystemAPI.GetComponentRO<LocalTransform>(playerCharacterEntity);
                
                cam.ValueRW.smoothingDuration =
                    GetSmoothingDuration(camRotation, characterTransform.ValueRO.Rotation, smoothingSpeed);
                cam.ValueRW.targetRotation = characterTransform.ValueRO.Rotation;

                return true;
            }

            return false;
        }

        private bool SetTargetRotation_Snap(RefRW<PlayerCameraComponent> cam, quaternion camRotation, float smoothingSpeed)
        {
            if (cam.ValueRO.smoothingDuration == 0)
            {
                float3 lookDirection = math.mul(camRotation, math.forward());
                lookDirection.y = 0;
                quaternion lookRotation = Quaternion.LookRotation(lookDirection);
                
                cam.ValueRW.smoothingDuration = GetSmoothingDuration(camRotation, lookRotation, smoothingSpeed);
                cam.ValueRW.targetRotation = lookRotation;

                return true;
            }

            return false;
        }
    }
}