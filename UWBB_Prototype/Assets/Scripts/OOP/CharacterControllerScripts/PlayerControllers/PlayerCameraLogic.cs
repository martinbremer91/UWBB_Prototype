using UnityEngine;
using UWBB.GameFramework;

namespace UWBB.CharacterController
{
    public class PlayerCameraLogic
    {
        private PlayerInputLogic inputController;
        private PlayerLockOnLogic lockOnLogic;
        private PlayerMovementLogic playerMovementLogic;
        private Transform player;
        private Transform camera;

        private CharacterControllerConfigs configs;
        private float rotationSpeed => configs.cameraRotationSpeed;

        private bool smoothingToHorizon;
        private float horizonSmoothingTarget;
        private float smoothingVelocity;
        private float smoothTimer;
        private readonly float smoothDuration = .1f;

        public void Init(Player p)
        {
            player = p.transform;
            camera = p.cameraTransform;
            configs = Main.instance.configs.ccConfigs;
        }
        
        public PlayerCameraData RunUpdate(InputState inputState) 
            => GetCameraLogicData(inputState);

        private PlayerCameraData GetCameraLogicData(InputState inputState)
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
            
            PlayerCameraData data = new()
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

        private PlayerCameraData GetHorizonSmoothingCameraData()
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

    public struct PlayerCameraData
    {
        public Vector3 pivotPoint { get; set; }
        public Vector3 rotationXAxis { get; set; }
        public float angleX { get; set; }
        public Vector3 rotationYAxis { get; set; }
        public float angleY { get; set; }
    }
}
