using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace BoidsExperiment
{
    [BurstCompile]
    public partial class BoidsOutOfBoundsSystem : SystemBase
    {
        private float3 center;
        private float3 volume;
        
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<BoidVolume>();
            RequireForUpdate<BoidSpawner>();
        }
        
        [BurstCompile]
        protected override void OnStartRunning()
        {
            var boidVolume = SystemAPI.GetSingleton<BoidVolume>();
            center = boidVolume.center;
            volume = boidVolume.volume;
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            new RestrictBoidsToVolumeJob
            {
                volumeCenter = center,
                boundsDimensions = new float3(volume.x * .5f + 1, volume.y * .5f + 1, volume.z * .5f +1),
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct RestrictBoidsToVolumeJob : IJobEntity
    {
        public float3 volumeCenter;
        public float3 boundsDimensions;
        
        private void Execute(in BoidDirection direction, ref LocalToWorld transform)
        {
            float3 forwardPoint = transform.Position + direction.value;

            bool outOfBounds =
                math.abs(forwardPoint.x) > volumeCenter.x + boundsDimensions.x
                || math.abs(forwardPoint.y) > volumeCenter.y + boundsDimensions.y
                || math.abs(forwardPoint.z) > volumeCenter.z + boundsDimensions.z;

            if (outOfBounds)
            {
                float3 reflectedPosition = volumeCenter + (volumeCenter - transform.Position);

                transform = new LocalToWorld
                {
                    Value = float4x4.TRS(reflectedPosition, transform.Rotation, new float3(1, 1, 1)
                )};
            }
        }
    }
}