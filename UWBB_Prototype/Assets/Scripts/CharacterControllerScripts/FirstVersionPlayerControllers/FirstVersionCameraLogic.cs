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

        public void Init(Player p)
        {
            player = p.transform;
            camera = p.camTransform;
        }
        
        public ICameraLogicData RunUpdate(IInputState inputState) 
            => RunUpdate((FirstVersionInputState)inputState);

        public FirstVersionCameraData RunUpdate(FirstVersionInputState inputState) 
            => GetCameraLogicData(inputState.characterAxisInput);

        private void LateUpdate(FirstVersionInputState inputState)
        {
            // if (inputController.FirstVersionInputState.snapCommand)
            // {
            //     SnapCamToHorizonPlane();
            //     inputController.FirstVersionInputState.snapCommand = false;
            // } else if (!lockOnLogic.lockedOn)
            //    GetCameraLogicData(inputState.characterAxisInput);
            // else
            //     OnLockOn();
        }

        private FirstVersionCameraData GetCameraLogicData(Vector2 input)
        {
            FirstVersionCameraData data = new();
            data.pivotPoint = player.position;
            
            data.rotationXAxis = Vector3.up;
            data.angleX = input.x * (rotationSpeed * Time.deltaTime);
            // camera.RotateAround(player.position, Vector3.up, input.x * (rotationSpeed * Time.deltaTime));

            data.rotationYAxis = camera.right;
            data.angleY = input.y * (rotationSpeed * Time.deltaTime);
            // if (Mathf.Abs(input.y) >= .75f && Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * 10) < 88)
            //     camera.RotateAround(player.position, camera.right, input.y * (rotationSpeed * Time.deltaTime));
            return data;
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
