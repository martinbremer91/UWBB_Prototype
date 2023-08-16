using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS_Test_Scripts.ComponentsAndTags
{
    public readonly partial struct SpawnerAspect : IAspect
    {
        public readonly Entity entity;
        
        private readonly RefRO<LocalTransform> transform;
        private LocalTransform Transform => transform.ValueRO;
        public float3 Position => Transform.Position;
        
        private readonly RefRO<SpawnerProperties> spawnerProperties;
        private readonly RefRW<SpawnerRandom> spawnerRandom;

        public int numberOfPrefabsToSpawn => spawnerProperties.ValueRO.numberOfObjectsToSpawn;
        public Entity prefabToSpawn => spawnerProperties.ValueRO.prefab;

        public LocalTransform GetRandomPrefabTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(0.5f)
            };
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = spawnerRandom.ValueRW.value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(Transform.Position, randomPosition) <= BRAIN_SAFETY_RADIUS_SQ);

            return randomPosition;
        }
        private const float BRAIN_SAFETY_RADIUS_SQ = 100;
        
        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = spawnerProperties.ValueRO.fieldDimensions.x * 0.5f,
            y = 0f,
            z = spawnerProperties.ValueRO.fieldDimensions.y * 0.5f
        };
        
        private quaternion GetRandomRotation() => quaternion.RotateY(spawnerRandom.ValueRW.value.NextFloat(-0.25f, 0.25f));
        private float GetRandomScale(float min) => spawnerRandom.ValueRW.value.NextFloat(min, 1f);
        
        public float2 GetRandomOffset()
        {
            return spawnerRandom.ValueRW.value.NextFloat2();
        }

        // public float ZombieSpawnTimer
        // {
        //     get => _zombieSpawnTimer.ValueRO.Value;
        //     set => _zombieSpawnTimer.ValueRW.Value = value;
        // }

        // public bool TimeToSpawnZombie => ZombieSpawnTimer <= 0f;
        //
        // public float ZombieSpawnRate => _graveyardProperties.ValueRO.ZombieSpawnRate;
        //
        // public Entity ZombiePrefab => _graveyardProperties.ValueRO.ZombiePrefab;

        // public LocalTransform GetZombieSpawnPoint()
        // {
        //     var position = GetRandomZombieSpawnPoint();
        //     return new LocalTransform
        //     {
        //         Position = position,
        //         Rotation = quaternion.RotateY(MathHelpers.GetHeading(position, Transform.Position)),
        //         Scale = 1f
        //     };
        // }

        // private float3 GetRandomZombieSpawnPoint()
        // {
        //     return GetZombieSpawnPoint(_graveyardRandom.ValueRW.Value.NextInt(ZombieSpawnPointCount));
        // }

        // private float3 GetZombieSpawnPoint(int i) => _zombieSpawnPoints.ValueRO.Value.Value.Value[i];
    }
}