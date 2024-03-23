using Unity.Entities;
using UnityEngine;
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
                Entity ccConfigs = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(ccConfigs, new CharacterControllerConfigsComponent()
                {
                    speed = authoring.configs.ccConfigs.firstVersionControllerSettings.speed,
                });
            }
        }
    }
    
    public struct CharacterControllerConfigsComponent : IComponentData
    {
        public float speed;
        // public float cameraRotationSpeed;
        // public float lockOnAngleTolerance;
        // public float lockOnRange;
        // public float yRotationDeadZoneAngle;
        // public float minAngleToYRotationDeadZone;
    }
}