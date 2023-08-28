using System;
using System.Linq;
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

                float partitionSize = authoring.partitionSize;

                float3 center = (float3)authoring.transform.position;
                float3 volume = (float3)authoring.volume;
                
                uint xPartitionCount = (uint)math.round(volume.x / authoring.partitionSize);
                uint yPartitionCount = (uint)math.round(volume.y / authoring.partitionSize);
                uint zPartitionCount = (uint)math.round(volume.z / authoring.partitionSize);
                
                uint partitionsCount = xPartitionCount * yPartitionCount * zPartitionCount;
                int partitionCollectionBufferSize = (int)math.round(partitionsCount);

                if (partitionsCount > 10000)
                    throw new Exception("Partition count must not exceed 10 thousand");
                
                AddComponent(entity, new BoidVolume
                {
                    center = center,
                    volume = authoring.volume,
                });

                Entity partitionsEntity = GetEntity(TransformUsageFlags.None);
                AddComponent(partitionsEntity, new BoidPartitionsCollection());
                
                var collectionBuffer = AddBuffer<BoidPartitionsCollectionBuffer>(partitionsEntity);
                collectionBuffer.ResizeUninitialized(partitionCollectionBufferSize);

                float3 startingPartitionCoords = center - volume * .5f;
                float3 currentPartitionCoords = startingPartitionCoords;
                
                for (int x = 0; x < xPartitionCount; x++)
                {
                    currentPartitionCoords.x = startingPartitionCoords.x + x * partitionSize;

                    for (int y = 0; y < yPartitionCount; y++)
                    {
                        currentPartitionCoords.y = startingPartitionCoords.y + y * partitionSize;
                        
                        for (int z = 0; z < zPartitionCount; z++)
                        {
                            currentPartitionCoords.z = startingPartitionCoords.z + z * partitionSize;

                            int uniqueHash = currentPartitionCoords.GetUniqueHashCode();

                            if (collectionBuffer.AsNativeArray().Any(b => b.value.partitionHash == uniqueHash))
                                Debug.LogError("Non-unique hash detected: " + currentPartitionCoords + " => " + uniqueHash);

                            collectionBuffer.Add(new BoidPartitionsCollectionBuffer
                            {
                                value = new BoidPartitionBuffer{ partitionHash = uniqueHash }
                            });
                        }
                    }
                }
            }
        }
    }

    public struct BoidVolume : IComponentData
    {
        public float3 center;
        public float3 volume;
    }

    public struct BoidPartitionsCollection : IComponentData {}
    
    public struct BoidPartitionsCollectionBuffer : IBufferElementData
    {
        public BoidPartitionBuffer value;
    }
    
    public struct BoidPartitionBuffer : IBufferElementData
    {
        public int partitionHash;
    }
}