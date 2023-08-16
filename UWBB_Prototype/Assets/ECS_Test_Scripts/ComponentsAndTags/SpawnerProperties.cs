﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS_Test_Scripts.ComponentsAndTags
{
    public struct SpawnerProperties : IComponentData
    {
        public float2 fieldDimensions;
        public int numberOfObjectsToSpawn;
        public Entity prefab;
    }
}