using System.Globalization;
using MBre.Utilities;
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
        private Transform camera;

        private readonly float rotationSpeed = 180;

        private bool smoothingToHorizon;
        private float horizonSmoothingTarget;
        private float smoothingVelocity;
        private float smoothTimer;
        private readonly float smoothDuration = .1f;

        public void Init(Player p)
        {
            player = p.transform;
            camera = p.cameraTransform;
        }
        
        public ICameraLogicData RunUpdate(IInputState inputState) 
            => RunUpdate((FirstVersionInputState)inputState);

        public FirstVersionCameraData RunUpdate(FirstVersionInputState inputState) 
            => GetCameraLogicData(inputState);

        private void LateUpdate(FirstVersionInputState inputState)
        {
            // } else if (!lockOnLogic.lockedOn)
            //    GetCameraLogicData(inputState.characterAxisInput);
            // else
            //     OnLockOn();
        }

        private FirstVersionCameraData GetCameraLogicData(FirstVersionInputState inputState)
        {
            if (inputState.snapCommand)
            {
                smoothingToHorizon = true;
                smoothTimer = 0;
                horizonSmoothingTarget = GetAngleToHorizonPlane();
            }

            if (smoothingToHorizon)
                return GetHorizonSmoothingCameraData();

            Vector2 input = inputState.characterAxisInput;
            bool camAngleLimitReached =
                Mathf.Abs(input.y) >= .75f && Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * 10) < 88;
            
            FirstVersionCameraData data = new()
            {
                pivotPoint = player.position,
                rotationXAxis = Vector3.up,
                angleX = input.x * (rotationSpeed * Time.deltaTime),
                rotationYAxis = camera.right,
                angleY = GetYRotationAngle(camAngleLimitReached, input)
            };
            
            return data;
        }

        private float GetAngleToHorizonPlane()
        {
            var forward = camera.forward;
            float angleToHorizonPlane = Vector3.Angle(forward, new Vector3(forward.x, 0, forward.z));
            return forward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
        }

        private float GetYRotationAngle(bool camAngleLimitReached, Vector2 input) 
            => camAngleLimitReached ? input.y * (rotationSpeed * Time.deltaTime) : 0;

        private FirstVersionCameraData GetHorizonSmoothingCameraData()
        {
            bool finishedSmoothing = smoothTimer >= smoothDuration;
            smoothTimer += Time.deltaTime;
            smoothingToHorizon = !finishedSmoothing;
            
            float value = finishedSmoothing ? 0 : (horizonSmoothingTarget / smoothDuration) * Time.deltaTime;
            
            return new()
            {
                pivotPoint = player.position,
                rotationXAxis = Vector3.up,
                angleX = 0,
                rotationYAxis = camera.right,
                angleY = value
            };
        }
        
        private void OnLockOn()
        {
            // Vector3 targetPlayerDirection = (player.position - lockOnController.target.position).normalized;
            // float distanceToPlayer = (player.position - transform.position).magnitude;
            // transform.position = player.position + targetPlayerDirection * distanceToPlayer;
            // transform.LookAt(lockOnController.target.position);
        }
    }

    public struct FirstVersionCameraData : ICameraLogicData
    {
        public Vector3 pivotPoint { get; set; }
        public Vector3 rotationXAxis { get; set; }
        public float angleX { get; set; }
        public Vector3 rotationYAxis { get; set; }
        public float angleY { get; set; }
    }
}
