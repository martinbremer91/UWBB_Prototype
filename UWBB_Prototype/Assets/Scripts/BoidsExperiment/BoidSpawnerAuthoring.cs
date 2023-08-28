using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoidSpawnerAuthoring : MonoBehaviour
{
    public GameObject boidPrefab;
    public int numberOfBoids;
    public Vector3 spawnerVolume;

    public class Baker : Baker<BoidSpawnerAuthoring>
    {
        public override void Bake(BoidSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity, new BoidSpawner
            {
                boidPrefab = GetEntity(authoring.boidPrefab, TransformUsageFlags.None),
                numberOfBoids = authoring.numberOfBoids,
                spawnerVolume = authoring.spawnerVolume,
                center = authoring.transform.position,
            });
        }
    }
}

public struct BoidSpawner : IComponentData
{
    public Entity boidPrefab;
    public int numberOfBoids;
    public float3 spawnerVolume;
    public float3 center;
}
