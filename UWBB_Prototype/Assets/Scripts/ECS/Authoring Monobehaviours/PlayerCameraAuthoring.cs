using Unity.Entities;
using UnityEngine;
using UWBB.Components;

namespace ECS
{
    public class PlayerCameraAuthoring : MonoBehaviour
    {
        private class CameraAuthoringBaker : Baker<PlayerCameraAuthoring>
        {
            public override void Bake(PlayerCameraAuthoring authoring)
            {
                if (!Application.isPlaying)
                    return;

                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new PlayerCameraTagComponent());
            }
        }
    }
}