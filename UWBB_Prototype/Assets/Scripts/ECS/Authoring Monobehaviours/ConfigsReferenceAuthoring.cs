using Unity.Entities;
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
                    cameraRotationSpeed = ccConfigs.cameraRotationSpeed,
                    cameraSnapSpeed = ccConfigs.cameraSnapSpeed,
                });
            }
        }
    }
    
    public struct CharacterControllerConfigsComponent : IComponentData
    {
        public float speed;
        public float cameraRotationSpeed;
        public float cameraSnapSpeed;
        // public float lockOnAngleTolerance;
        // public float lockOnRange;
        // public float yRotationDeadZoneAngle;
        // public float minAngleToYRotationDeadZone;
    }
}