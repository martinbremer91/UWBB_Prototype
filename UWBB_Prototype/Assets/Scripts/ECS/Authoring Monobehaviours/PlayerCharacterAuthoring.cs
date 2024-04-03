using Unity.Entities;
using UnityEngine;
using UWBB.Components;

namespace ECS
{
    public class PlayerCharacterAuthoring : MonoBehaviour
    {
        private class PlayerCharacterAuthoringBaker : Baker<PlayerCharacterAuthoring>
        {
            public override void Bake(PlayerCharacterAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new PlayerCharacterComponent());
            }
        }
    }
}