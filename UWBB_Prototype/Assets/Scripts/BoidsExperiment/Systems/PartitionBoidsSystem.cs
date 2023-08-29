using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace BoidsExperiment
{
    [BurstCompile]
    public partial class PartitionBoidsSystem : SystemBase
    {
        private BoidPartitionsCollection partitions;
        
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<BoidPartitionsCollection>();
            RequireForUpdate<BoidDirection>();
        }
    
        // [BurstCompile]
        // protected override void OnStartRunning()
        // {
        //     partitions = SystemAPI.GetSingleton<BoidPartitionsCollection>();
        // }
    
        [BurstCompile]
        protected override void OnUpdate()
        {
            // new PartitionBoidsJob { partitions =  partitions }.ScheduleParallel();
        }

        public static uint GetMonoDimensionalPartitionIndex(uint3 index3D, uint2 size) 
            => index3D.x + index3D.y * (size.x - 1) + index3D.z * (size.x * size.y - 1);
    }
    
    [BurstCompile]
    public partial struct PartitionBoidsJob : IJobEntity
    {
        public BoidPartitionsCollection partitions;
        
        private void Execute()
        {
            
        }
    }
}