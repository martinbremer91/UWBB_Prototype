using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UWBB.Components;

namespace UWBB.Systems
{
    public partial struct PlayerCameraModeSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerInputComponent>();
            state.RequireForUpdate<PlayerCameraComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            PlayerInputComponent playerInput = SystemAPI.GetSingleton<PlayerInputComponent>();
            RefRW<PlayerCameraComponent> cam = SystemAPI.GetSingletonRW<PlayerCameraComponent>();

            if (playerInput.lockOnCommand)
                SetPlayerCameraMode(cam, PlayerCameraMode.Reset);
            else if (playerInput.snapCommand) 
                SetPlayerCameraMode(cam, PlayerCameraMode.SnapToHorizon);
        }

        private void SetPlayerCameraMode(RefRW<PlayerCameraComponent> cam, PlayerCameraMode mode)
        {
            cam.ValueRW.mode = mode;
            cam.ValueRW.smoothingDuration = 0;
            cam.ValueRW.smoothingTimer = 0;
            cam.ValueRW.targetRotation = new quaternion();
        }
    }
}