using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BoidsExperiment
{
    [RequireComponent(typeof(BoidSpawnerAuthoring))]
    public class BoidVolumeAndPartitionsAuthoring : MonoBehaviour
    {
        public float partitionSize = 10;
        public Vector3 volume = new Vector3(200, 200, 200);

        public class Baker : Baker<BoidVolumeAndPartitionsAuthoring>
        {
            public override void Bake(BoidVolumeAndPartitionsAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new BoidVolumeAndPartitions
                {
                    partitionSize = authoring.partitionSize,
                    center = authoring.transform.position,
                    volume = authoring.volume,
                });
            }
        }
    }

    public struct BoidVolumeAndPartitions : IComponentData
    {
        public float partitionSize;
        public float3 center;
        public float3 volume;
    }
}