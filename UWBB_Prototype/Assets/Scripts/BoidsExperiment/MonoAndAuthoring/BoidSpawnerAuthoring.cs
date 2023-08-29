using BoidsExperiment;
using Unity.Entities;
using UnityEngine;

[RequireComponent(typeof(BoidVolumeAndPartitionsAuthoring))]
public class BoidSpawnerAuthoring : MonoBehaviour
{
    public GameObject boidPrefab;
    public int numberOfBoids;

    public class Baker : Baker<BoidSpawnerAuthoring>
    {
        public override void Bake(BoidSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity, new BoidSpawner
            {
                boidPrefab = GetEntity(authoring.boidPrefab, TransformUsageFlags.None),
                numberOfBoids = authoring.numberOfBoids,
            });
        }
    }
}

public struct BoidSpawner : IComponentData
{
    public Entity boidPrefab;
    public int numberOfBoids;
}
