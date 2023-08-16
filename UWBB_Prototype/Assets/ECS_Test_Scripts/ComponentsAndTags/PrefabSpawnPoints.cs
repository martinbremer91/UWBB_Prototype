using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS_Test_Scripts.ComponentsAndTags
{
    public struct PrefabSpawnPoints : IComponentData
    {
        public BlobAssetReference<PrefabSpawnPointsBlob> Value;
    }
    
    public struct PrefabSpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}