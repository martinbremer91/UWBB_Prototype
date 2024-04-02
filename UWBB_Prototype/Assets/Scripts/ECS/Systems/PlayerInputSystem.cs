using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UWBB.Components;

namespace UWBB.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PlayerInputSystem : SystemBase
    {
        private FirstVersionControls controls;
        private FirstVersionControls.FreeMovementActions freeMovement;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTagComponent>();
            RequireForUpdate<PlayerInputComponent>();
            
            controls = new FirstVersionControls();
            freeMovement = controls.FreeMovement;
        }

        protected override void OnStartRunning()
        {
            controls.Enable();
        }

        protected override void OnUpdate()
        {
            PlayerInputComponent currentInput = new PlayerInputComponent();
            
            currentInput.characterPlaneInput = freeMovement.XZMovement.ReadValue<Vector2>();
            currentInput.worldYInput = (int)math.round(freeMovement.YMovement.ReadValue<float>());
            currentInput.characterAxisInput = freeMovement.CameraStick.ReadValue<Vector2>();
            
            currentInput.dashCommand = freeMovement.Dash.WasPressedThisFrame();
            currentInput.snapCommand = freeMovement.SnapToHorizon.WasPressedThisFrame();
            currentInput.lockOnCommand = freeMovement.LockOnCommand.WasPressedThisFrame();
            
            SystemAPI.SetSingleton(currentInput);
        }
        
        protected override void OnStopRunning()
        {
            controls.Disable();
        }
    }
}