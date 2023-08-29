using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace BoidsExperiment
{
    [BurstCompile]
    public partial class AssignHashFromBoidPositionSystem : SystemBase
    {
        private float3 centerOffset;
        private float partitionSize;
        private uint2 xyPartitionsCount;
        
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<BoidPartitionsCollection>();
            RequireForUpdate<BoidDirection>();
        }

        [BurstCompile]
        protected override void OnStartRunning()
        {
            BoidVolume volume = SystemAPI.GetSingleton<BoidVolume>();
            centerOffset = -(volume.center - volume.volume * .5f);
            partitionSize = volume.partitionSize;
            xyPartitionsCount = volume.xyPartitionsCount;
        }
        
        protected override void OnUpdate()
        {
            new GetHashFromBoidPositionJob
            {
                offset = centerOffset,
                partitionSize = partitionSize,
                xySizes = xyPartitionsCount,
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct GetHashFromBoidPositionJob : IJobEntity
    {
        public float3 offset;
        public float partitionSize;
        public uint2 xySizes;
        
        private void Execute(ref BoidPartitionHash hash, in LocalToWorld transform)
        {
            float3 partitionGridPosition = transform.Position + offset;
            
            uint3 partitionIndices = new uint3(
                (uint)math.floor(partitionGridPosition.x / partitionSize),
                (uint)math.floor(partitionGridPosition.y / partitionSize),
                (uint)math.floor(partitionGridPosition.z / partitionSize));

            hash.value = PartitionBoidsSystem.GetMonoDimensionalPartitionIndex(partitionIndices, xySizes);
        }
    }
}