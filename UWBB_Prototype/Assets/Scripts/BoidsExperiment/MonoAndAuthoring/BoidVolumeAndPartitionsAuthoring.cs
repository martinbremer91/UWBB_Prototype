using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BoidsExperiment
{
    [RequireComponent(typeof(BoidSpawnerAuthoring))]
    public class BoidVolumeAndPartitionsAuthoring : MonoBehaviour
    {
        public BoidsConfigs configs;
        
        private float partitionSize => configs.range;
        public Vector3 volume = new Vector3(200, 200, 200);

        public class Baker : Baker<BoidVolumeAndPartitionsAuthoring>
        {
            public override void Bake(BoidVolumeAndPartitionsAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);

                float3 center = (float3)authoring.transform.position;
                float3 volume = (float3)authoring.volume;
                float partitionSize = authoring.partitionSize;
                
                uint xPartitionCount = (uint)math.round(volume.x / partitionSize);
                uint yPartitionCount = (uint)math.round(volume.y / partitionSize);
                uint zPartitionCount = (uint)math.round(volume.z / partitionSize);
                
                uint partitionsCount = xPartitionCount * yPartitionCount * zPartitionCount;
                int partitionCollectionBufferSize = (int)math.round(partitionsCount);

                if (partitionsCount > 10000)
                    throw new Exception("Partition count must not exceed 10 thousand");
                
                AddComponent(entity, new BoidVolume
                {
                    center = center,
                    volume = authoring.volume,
                    partitionSize = partitionSize,
                    xyPartitionsCount = new uint2(xPartitionCount, yPartitionCount),
                });

                Entity partitionsEntity = GetEntity(TransformUsageFlags.None);
                AddComponent(partitionsEntity, new BoidPartitionsCollection());
                
                var collectionBuffer = AddBuffer<BoidPartitionsCollectionBuffer>(partitionsEntity);
                collectionBuffer.ResizeUninitialized(partitionCollectionBufferSize);

                uint2 xySizes = new uint2(xPartitionCount, yPartitionCount);

                for (uint z = 0; z < zPartitionCount; z++)
                {
                    for (uint y = 0; y < yPartitionCount; y++)
                    {
                        for (uint x = 0; x < xPartitionCount; x++)
                        {
                            uint uniqueHash = PartitionBoidsSystem.GetMonoDimensionalPartitionIndex(new uint3(x, y, z), xySizes);

                            // if (collectionBuffer.AsNativeArray().Any(b => b.value.partitionHash == uniqueHash))
                            //     Debug.LogError("Non-unique hash detected: " + " => " + uniqueHash);

                            collectionBuffer.Add(new BoidPartitionsCollectionBuffer
                            {
                                value = new BoidPartitionBuffer{ partitionHash = uniqueHash }
                            });
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 firstPartitionPos = -(volume * .5f) + Vector3.one * partitionSize * .5f;
            Vector3Int partitionSizes = new Vector3Int(
                Mathf.RoundToInt(volume.x / partitionSize),
                Mathf.RoundToInt(volume.y / partitionSize),
                Mathf.RoundToInt(volume.z / partitionSize));
            
            for (uint z = 0; z < partitionSizes.z; z++)
            {
                for (uint y = 0; y < partitionSizes.y; y++)
                {
                    for (uint x = 0; x < partitionSizes.x; x++)
                    {
                        Vector3 currentIndex = new Vector3(x, y, z);
                        Vector3 pos = firstPartitionPos + currentIndex * partitionSize;
                        Gizmos.DrawWireCube(pos, Vector3.one * partitionSize);
                    }
                }
            }
        }
    }

    public struct BoidVolume : IComponentData
    {
        public float3 center;
        public float3 volume;
        public float partitionSize;
        public uint2 xyPartitionsCount;
    }

    public struct BoidPartitionsCollection : IComponentData {}
    
    public struct BoidPartitionsCollectionBuffer : IBufferElementData
    {
        public BoidPartitionBuffer value;
    }
    
    public struct BoidPartitionBuffer : IBufferElementData
    {
        public uint partitionHash;
    }
}