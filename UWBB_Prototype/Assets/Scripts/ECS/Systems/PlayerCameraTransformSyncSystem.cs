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
        private Transform mbCamTransform;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerCameraTagComponent>();
        }

        protected override void OnStartRunning()
        {
            mbCamTransform = PlayerCamera.instance.transform;

            if (mbCamTransform == null)
                throw new NullReferenceException("Camera Transform cannot be null");
        }

        protected override void OnUpdate()
        {
            RefRO<LocalToWorld> cameraEntityLocalToWorld =
                SystemAPI.GetComponentRO<LocalToWorld>(SystemAPI.GetSingletonEntity<PlayerCameraTagComponent>());

            mbCamTransform.position = cameraEntityLocalToWorld.ValueRO.Position;
            mbCamTransform.rotation = cameraEntityLocalToWorld.ValueRO.Rotation;
        }
    }
}