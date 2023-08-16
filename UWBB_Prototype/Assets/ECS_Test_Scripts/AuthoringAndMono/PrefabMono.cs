using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace ECS_Test_Scripts.AuthoringAndMono
{
    public class PrefabMono : MonoBehaviour
    {
        public GameObject Renderer;
    }

    public class PrefabBaker : Baker<PrefabMono>
    {
        public override void Bake(PrefabMono authoring)
        {
            var prefabEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(prefabEntity, new PrefabRenderer
            {
                Value = GetEntity(authoring.Renderer, TransformUsageFlags.Dynamic)
            });
        }
    }
    
    [MaterialProperty("PrefabOffset")]
    public struct PrefabOffset : IComponentData
    {
        public float2 Value;
    }

    public struct PrefabRenderer : IComponentData
    {
        public Entity Value;
    }
}