using Unity.Burst;
using Unity.Entities;
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
                cam.ValueRW.mode = PlayerCameraMode.Reset;
            else if (playerInput.snapCommand)
                cam.ValueRW.mode = PlayerCameraMode.SnapToHorizon;
        }
    }
}