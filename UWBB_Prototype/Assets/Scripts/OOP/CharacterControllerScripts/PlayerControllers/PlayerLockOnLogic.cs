using System.Collections.Generic;
using UnityEngine;
using UWBB.GameFramework;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class PlayerLockOnLogic
    {
        private CharacterControllerConfigs configs;
        private float maxLockOnDotProduct 
            => 1 - configs.lockOnAngleTolerance;
        private float range => configs.lockOnRange;

        private Transform playerTransform;
        private Transform cameraTransform;
        private LockOnController lockOnController;

        public void Init(Player p)
        {
            playerTransform = p.transform;
            cameraTransform = p.cameraTransform;
            lockOnController = p.lockOnController;
            configs = Main.instance.configs.ccConfigs;
        }
        
        public PlayerLockOnData RunUpdate(InputState inputState) 
            => ProcessLockOn(inputState);

        private PlayerLockOnData ProcessLockOn(InputState inputState)
        {
            if (lockOnController.lockedOn == inputState.lockOnCommand)
                return default;
            
            ILockOnTarget activeTarget = GetActiveTarget(lockOnController.activeTarget != null);
            if (activeTarget == null)
                return default;
            
            return new() { lockedOn = true, target = activeTarget };
        }

        private ILockOnTarget GetActiveTarget(bool hasTarget)
        {
            if (hasTarget)
            {
                bool targetIsValid = IsTargetValid(lockOnController.activeTarget);
                return targetIsValid ? lockOnController.activeTarget : null;
            }
            
            ILockOnTarget[] validTargets = GetValidTargets();
            if (validTargets.Length == 0)
                return null;
            return SelectActiveTarget(validTargets);
        }

        private bool IsTargetValid(float distance, float dotProduct)
        {
            // TODO: check target visibility
            return distance <= range && dotProduct >= maxLockOnDotProduct;
        }

        private bool IsTargetValid(ILockOnTarget lockOnTarget)
        {
            float distance = Vector3.Distance(lockOnTarget.position, playerTransform.position);
            Vector3 directionToTarget = (lockOnTarget.position - cameraTransform.position).normalized;
            float dotProduct = Vector3.Dot(directionToTarget, cameraTransform.forward);
            return IsTargetValid(distance, dotProduct);
        }

        private ILockOnTarget[] GetValidTargets()
        {
            List<ILockOnTarget> validTargets = new();
            
            for (int i = 0; i < lockOnController.lockOnTargets.Count; i++)
            {
                float distance = lockOnController.distancesToPlayer[i][0];
                float dotProduct = lockOnController.dotProductsToCamera[i][0];
                
                if (IsTargetValid(distance, dotProduct))
                    validTargets.Add(lockOnController.lockOnTargets[i]);
            }

            return validTargets.ToArray();
        }

        private ILockOnTarget SelectActiveTarget(ILockOnTarget[] validTargets)
        {
            ILockOnTarget closestTarget = null;
            float shortestDistance = Mathf.Infinity;
            
            foreach (var target in validTargets)
            {
                float distance = Vector3.Distance(target.position, playerTransform.position);
                if (shortestDistance > distance)
                {
                    closestTarget = target;
                    shortestDistance = distance;
                }
            }
            
            return closestTarget;
        }
    }

    public struct PlayerLockOnData
    {
        public bool lockedOn { get; set; }
        public ILockOnTarget target { get; set; }
    }
}