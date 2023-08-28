using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace BoidsExperiment
{
    [BurstCompile]
    public partial struct MoveBoidForwardSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BoidSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var moveJob = new MoveBoidForwardJob {deltaTime = SystemAPI.Time.DeltaTime};
            moveJob.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MoveBoidForwardJob : IJobEntity
    {
        public float deltaTime;

        private void Execute(in BoidSpeed speed, ref LocalToWorld transform)
        {
            transform = new LocalToWorld
            {
                Value = float4x4.TRS(
                    transform.Position + transform.Forward * speed.value * deltaTime,
                    transform.Rotation,
                    new float3(1, 1, 1))
            };
        }
    }
}