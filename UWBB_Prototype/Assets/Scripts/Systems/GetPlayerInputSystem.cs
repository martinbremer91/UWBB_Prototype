using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class GetPlayerInputSystem : SystemBase
    {
        private Entity playerEntity;
        private readonly DefaultMovement defaultMovement = new DefaultMovement();

        protected override void OnCreate()
        {
            RequireForUpdate<Player>();
        }

        protected override void OnStartRunning()
        {
            playerEntity = SystemAPI.GetSingletonEntity<Player>();
            defaultMovement.Enable();

            defaultMovement.FreeMovement.SnapToHorizon.performed += OnSnapToHorizonPlane;
            defaultMovement.FreeMovement.LockOnToggle.performed += OnLockOnToggle;
        }

        protected override void OnUpdate()
        {
            var playerPlaneInput = defaultMovement.FreeMovement.XZMovement.ReadValue<Vector2>();
            var cameraInput = defaultMovement.FreeMovement.YMovementandRotation.ReadValue<Vector2>();

            SystemAPI.SetSingleton(new PlayerInput
            {
                playerPlaneInput = playerPlaneInput,
                cameraInput = cameraInput,
            });
        }

        protected override void OnStopRunning()
        {
            defaultMovement.Disable();
            playerEntity = Entity.Null;
        }

        private void OnSnapToHorizonPlane(InputAction.CallbackContext obj)
        {
            if (!SystemAPI.Exists(playerEntity))
                return;

            SystemAPI.SetComponentEnabled<SnapToHorizonPlane>(playerEntity, true);
        }

        private void OnLockOnToggle(InputAction.CallbackContext obj)
        {
            if (!SystemAPI.Exists(playerEntity))
                return;

            bool lockedOn = SystemAPI.IsComponentEnabled<LockOnToggle>(playerEntity);
            SystemAPI.SetComponentEnabled<LockOnToggle>(playerEntity, !lockedOn);
        }
    }
}