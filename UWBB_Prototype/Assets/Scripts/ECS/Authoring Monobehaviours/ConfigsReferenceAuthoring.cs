﻿using Unity.Entities;
using UnityEngine;
using UWBB.CharacterController;
using UWBB.GameFramework;

namespace ECS
{
    public class ConfigsReferenceAuthoring : MonoBehaviour
    {
        [SerializeField] private MainConfigs configs;
        
        private class ConfigsReferenceAuthoringBaker : Baker<ConfigsReferenceAuthoring>
        {
            public override void Bake(ConfigsReferenceAuthoring authoring)
            {
                Entity ccConfigsEntity = GetEntity(TransformUsageFlags.Dynamic);

                CharacterControllerConfigs ccConfigs = authoring.configs.ccConfigs;
                AddComponent(ccConfigsEntity, new CharacterControllerConfigsComponent()
                {
                    speed = ccConfigs.speed,
                    rotationSpeed = ccConfigs.rotationSpeed / 100,
                    cameraRotationSpeed = ccConfigs.cameraRotationSpeed,
                    cameraSmoothingSpeed = ccConfigs.cameraSmoothingSpeed,
                });
            }
        }
    }
    
    public struct CharacterControllerConfigsComponent : IComponentData
    {
        public float speed;
        public float rotationSpeed;
        public float cameraRotationSpeed;
        public float cameraSmoothingSpeed;
        // public float lockOnAngleTolerance;
        // public float lockOnRange;
        // public float yRotationDeadZoneAngle;
        // public float minAngleToYRotationDeadZone;
    }
}