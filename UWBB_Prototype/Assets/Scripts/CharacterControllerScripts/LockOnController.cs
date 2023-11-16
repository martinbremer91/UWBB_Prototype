using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class LockOnController : IInitializable<Player>
    {
        private Transform playerTransform;
        private Transform cameraTransform;

        public List<ILockOnTarget> lockOnTargets = new();
        public ILockOnTarget activeTarget;

        public bool lockedOn { get; private set; }
        
        public NativeArray<float>[] distancesToPlayer;
        public NativeArray<float>[] dotProductsToCamera;
        private JobHandle[] jobHandles;

        private bool pendingCompleteJobs;
        private bool pendingDisposeArrays;
        
        public void Init(Player p)
        {
            playerTransform = p.transform;
            cameraTransform = p.cameraTransform;
        }
        
        public void ValidateTargets()
        {
            if (lockedOn)
                return;
            
            int targetsCount = lockOnTargets.Count;
            jobHandles = new JobHandle[targetsCount];
            
            distancesToPlayer = new NativeArray<float>[targetsCount];
            dotProductsToCamera = new NativeArray<float>[targetsCount];

            for (var i = 0; i < targetsCount; i++)
            {
                NativeArray<float> distanceToPlayer = new NativeArray<float>(1, Allocator.TempJob);
                NativeArray<float> dotProductToCamera = new NativeArray<float>(1, Allocator.TempJob);
                
                var target = lockOnTargets[i];
                LockOnValidationJob validationJob = new()
                {
                    targetPosition = target.position,
                    playerPosition = playerTransform.position,
                    cameraPosition = cameraTransform.position,
                    cameraForward = cameraTransform.forward,
                    distanceToPlayer = distanceToPlayer,
                    dotProductToCamera = dotProductToCamera
                };

                distancesToPlayer[i] = distanceToPlayer;
                dotProductsToCamera[i] = dotProductToCamera;
                
                jobHandles[i] = validationJob.Schedule();
            }

            pendingCompleteJobs = true;
            pendingDisposeArrays = true;
        }

        public void CompleteValidationJobs()
        {
            if (!pendingCompleteJobs) return;
            foreach (var jobHandle in jobHandles) 
                jobHandle.Complete();
            pendingCompleteJobs = false;
        }

        public void ApplyLockOnLogicData(ILockOnLogicData lockOnData)
        {
            lockedOn = lockOnData.lockedOn;
            activeTarget = lockOnData.target;
        }

        public void DisposeNativeArrays()
        {
            if (!pendingDisposeArrays) return;
            foreach (var dist in distancesToPlayer) 
                dist.Dispose();
            foreach (var dot in dotProductsToCamera) 
                dot.Dispose();
            pendingDisposeArrays = false;
        }
        
        [BurstCompile]
        private struct LockOnValidationJob : IJob
        {
            public float3 targetPosition;
            public float3 playerPosition;
            public float3 cameraPosition;
            public float3 cameraForward;

            public NativeArray<float> distanceToPlayer;
            public NativeArray<float> dotProductToCamera;
            
            public void Execute()
            {
                distanceToPlayer[0] = math.distance(targetPosition, playerPosition);

                float3 directionToTarget = math.normalizesafe(targetPosition - cameraPosition);
                dotProductToCamera[0] = math.dot(directionToTarget, cameraForward);
            }
        }
    }
}