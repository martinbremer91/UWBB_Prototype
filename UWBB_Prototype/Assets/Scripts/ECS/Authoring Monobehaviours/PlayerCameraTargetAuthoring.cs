using Unity.Entities;
using UnityEngine;
using UWBB.Components;

namespace ECS
{
    public class PlayerCameraTargetAuthoring : MonoBehaviour
    {
        private class PlayerCameraTargetAuthoringBaker : Baker<PlayerCameraTargetAuthoring>
        {
            public override void Bake(PlayerCameraTargetAuthoring authoring)
            {
                if (!Application.isPlaying)
                    return;

                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerCameraTargetComponent());
            }
        }
    }
}