using System;
using ECS;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UWBB.Components;

namespace UWBB.Systems
{
    public partial class PlayerCameraTransformSyncSystem : SystemBase
    {
        private Transform camTransform;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerCameraTagComponent>();
        }

        protected override void OnStartRunning()
        {
            camTransform = PlayerCamera.instance.transform;

            if (camTransform == null)
                throw new NullReferenceException("Camera Transform cannot be null");
        }

        protected override void OnUpdate()
        {
            RefRO<LocalToWorld> cameraEntityLocalToWorld =
                SystemAPI.GetComponentRO<LocalToWorld>(SystemAPI.GetSingletonEntity<PlayerCameraTagComponent>());

            camTransform.position = cameraEntityLocalToWorld.ValueRO.Position;
            camTransform.rotation = cameraEntityLocalToWorld.ValueRO.Rotation;
        }
    }
}