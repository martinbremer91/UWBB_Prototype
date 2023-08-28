using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoidAuthoring : MonoBehaviour
{
    public float speed = 1;

    public class Baker : Baker<BoidAuthoring>
    {
        public override void Bake(BoidAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<BoidSpeed>(entity, new BoidSpeed {value =  authoring.speed});
            AddComponent<BoidDirection>(entity, new BoidDirection
            {
                direction = new float3(0, 0, 1),
                avoidanceDir = new float3(0, 0, 1),
                // alignmentDir = new float3(0, 0, 1),
                // cohesionDir = new float3(0, 0, 1),
                // avoidanceWeight = .3f,
                // alignmentWeight = .3f,
                // cohesionWeight = .3f,
            });
        }
    } 
}

public struct BoidSpeed : IComponentData
{
    public float value;
}

public struct BoidDirection : IComponentData
{
    public float3 direction;

    public float3 avoidanceDir;
    // public float3 alignmentDir;
    // public float3 cohesionDir;
    //
    // public float avoidanceWeight;
    // public float alignmentWeight;
    // public float cohesionWeight;
}
