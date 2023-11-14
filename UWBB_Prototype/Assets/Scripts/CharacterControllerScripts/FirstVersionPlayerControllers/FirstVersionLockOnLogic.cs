using System;
using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionLockOnLogic : 
        IPlayerLogic<IInputState, ILockOnLogicData>,
        IPlayerLogic<FirstVersionInputState, FirstVersionLockOnData>
    {
        private FirstVersionInputLogic inputController;
        
        public Transform target;
        private Transform cameraTransform;

        public bool lockedOn;

        [Tooltip("Tolerance of the angle between camera and target for lock on to work. Higher value => more tolerant. \n \n" +
                 "A value of 0 corresponds to a tolerance of 0°. \nA value of 1 corresponds to a tolerance of 180°.")]
        private float lockOnAngleTolerance;

        public Action onLockOn;
        
        private float maxLockOnDotProduct => 1 - lockOnAngleTolerance;

        public void Init(Player player)
        {
            
        }
        
        public ILockOnLogicData RunUpdate(IInputState inputState)
            => RunUpdate((FirstVersionInputState)inputState);

        public FirstVersionLockOnData RunUpdate(FirstVersionInputState inputState)
        {
            Debug.Log("FirstVersionLockOn RunUpdate");
            return default;
        }
        
        private void Update()
        {
            // if (inputController.FirstVersionInputState.lockOnToggleCommand)
            // {
            //     if (lockedOn)
            //         ReleaseLockOn();
            //     else
            //         TryLockOn();
            //     
            //     inputController.FirstVersionInputState.lockOnToggleCommand = false;
            // }
        }

        private void ReleaseLockOn()
        {
            lockedOn = false;
        }

        private void TryLockOn()
        {
            if (target == null)
                return;

            Vector3 directionToTarget = (target.position - cameraTransform.position).normalized;
            float camTargetDotProduct = Vector3.Dot(cameraTransform.forward, directionToTarget);
            lockedOn = camTargetDotProduct >= maxLockOnDotProduct;
            
            if (lockedOn)
                onLockOn?.Invoke();
        }
    }
    
    public struct FirstVersionLockOnData : ILockOnLogicData {}
}