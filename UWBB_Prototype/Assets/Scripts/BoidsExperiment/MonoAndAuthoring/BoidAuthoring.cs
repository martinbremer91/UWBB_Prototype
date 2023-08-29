using BoidsExperiment;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoidAuthoring : MonoBehaviour
{
    public BoidsConfigs configs;
    
    private float speed => configs.speed;
    private float range => configs.range;

    public class Baker : Baker<BoidAuthoring>
    {
        public override void Bake(BoidAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BoidPartitionHash { value = default });
            AddComponent(entity, new BoidSpeed {value =  authoring.speed});
            AddComponent(entity, new BoidDirection
            {
                value = new float3(0, 0, 1),
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

public struct BoidPartitionHash : IComponentData
{
    public uint value;
}

public struct BoidSpeed : IComponentData
{
    public float value;
}

public struct BoidDirection : IComponentData
{
    public float3 value;

    public float3 avoidanceDir;
    // public float3 alignmentDir;
    // public float3 cohesionDir;
    //
    // public float avoidanceWeight;
    // public float alignmentWeight;
    // public float cohesionWeight;
}
