using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionCameraLogic : 
        IPlayerLogic<IInputState, ICameraLogicData>,
        IPlayerLogic<FirstVersionInputState, FirstVersionCameraData>
    {
        private FirstVersionInputLogic inputController;
        private FirstVersionLockOnLogic lockOnLogic;
        private FirstVersionMovementLogic firstVersionMovementLogic;
        private Transform player;

        // private float rotationSpeed = 180;

        private void Awake() => lockOnLogic.onLockOn += OnLockOn;

        public ICameraLogicData RunUpdate(IInputState inputState) 
            => RunUpdate((FirstVersionInputState)inputState);

        public FirstVersionCameraData RunUpdate(FirstVersionInputState inputState)
        {
            Debug.Log("FirstVersionCamera RunUpdate");
            return default;
        }
        
        private void LateUpdate()
        {
            // if (inputController.FirstVersionInputState.snapCommand)
            // {
            //     SnapCamToHorizonPlane();
            //     inputController.FirstVersionInputState.snapCommand = false;
            // } else if (!lockOnLogic.lockedOn)
            //     HandleCamInput(inputController.FirstVersionInputState.characterAxisInput);
            // else
            //     OnLockOn();
        }

        private void HandleCamInput(Vector2 input)
        {
            // transform.RotateAround(player.position, Vector3.up, input.x * (rotationSpeed * Time.deltaTime));
            //
            // if (Mathf.Abs(input.y) >= .75f && Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * 10) < 88)
            //     transform.RotateAround(player.position, transform.right, input.y * (rotationSpeed * Time.deltaTime));
        }

        public Vector3 GetVectorInRelationToCamRotation(Vector2 vector)
        {
            // var tf = transform;
            // return tf.right * vector.x + tf.forward * vector.y;
            return default;
        }

        private void SnapCamToHorizonPlane()
        {
            // float angleToHorizonPlane = GetAngleToHorizonPlane();
            // transform.RotateAround(player.position, transform.right, angleToHorizonPlane);
            // firstVersionMovement.SnapPlayerToHorizonPlane();
        }
        
        private float GetAngleToHorizonPlane()
        {
            // float angleToHorizonPlane = Vector3.Angle(transform.forward, new Vector3(transform.forward.x, 0, transform.forward.z));
            // return transform.forward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
            return default;
        }

        private void OnLockOn()
        {
            // Vector3 targetPlayerDirection = (player.position - lockOnController.target.position).normalized;
            // float distanceToPlayer = (player.position - transform.position).magnitude;
            // transform.position = player.position + targetPlayerDirection * distanceToPlayer;
            // transform.LookAt(lockOnController.target.position);
        }

        private void OnDestroy()
        {
            lockOnLogic.onLockOn -= OnLockOn;
        }
    }
    
    public struct FirstVersionCameraData : ICameraLogicData {}
}
