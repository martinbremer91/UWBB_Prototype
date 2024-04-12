using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UWBB.Components;

namespace ECS
{
    public class PlayerCharacterModelAuthoring : MonoBehaviour
    {
        private class PlayerCharacterModelAuthoringBaker : Baker<PlayerCharacterModelAuthoring>
        {
            public override void Bake(PlayerCharacterModelAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerCharacterModelComponent
                {
                    translationDirection = float3.zero,
                });
            }
        }
    }
}