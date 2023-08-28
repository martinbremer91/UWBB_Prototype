using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace BoidsExperiment
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public partial struct RotateAndMoveBoidForwardSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BoidSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var rotateAndMoveJob = new RotateAndMoveBoidForwardJob {deltaTime = SystemAPI.Time.DeltaTime};
            rotateAndMoveJob.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct RotateAndMoveBoidForwardJob : IJobEntity
    {
        public float deltaTime;

        private void Execute(in BoidSpeed speed, in BoidDirection direction, ref LocalToWorld transform)
        {
            float3 translation = transform.Position + transform.Forward * speed.value * deltaTime;
            quaternion rotation = quaternion.LookRotationSafe(direction.direction, new float3(0, 0, 1));
            
            transform = new LocalToWorld
            {
                Value = float4x4.TRS(translation, rotation, new float3(1, 1, 1))
            };
        }
    }
}