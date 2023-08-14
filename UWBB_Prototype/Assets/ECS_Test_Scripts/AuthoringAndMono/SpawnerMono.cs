using ECS_Test_Scripts.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace ECS_Test_Scripts.AuthoringAndMono
{
    public class SpawnerMono : MonoBehaviour
    {
        public float2 fieldDimensions;
        public int numberOfObjectsToSpawn;
        public  GameObject prefab;
        public uint randomSeed;
    }

    public class SpawnerBaker : Baker<SpawnerMono>
    {
        public override void Bake(SpawnerMono authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity, new SpawnerProperties
            {
                fieldDimensions = authoring.fieldDimensions,
                numberOfObjectsToSpawn = authoring.numberOfObjectsToSpawn,
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.None)
            });
            AddComponent(entity, new SpawnerRandom
            {
                value = Random.CreateFromIndex(authoring.randomSeed)
            });
        }
    }
}