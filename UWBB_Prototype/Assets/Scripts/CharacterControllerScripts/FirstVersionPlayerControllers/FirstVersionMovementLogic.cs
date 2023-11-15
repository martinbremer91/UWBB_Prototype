using UnityEngine;
using UWBB.GameFramework;
using UWBB.Interfaces;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionMovementLogic : 
        IPlayerLogic<IInputState, IMovementLogicData>,
        IPlayerLogic<FirstVersionInputState, FirstVersionMovementData>
    {
        private FirstVersionInputLogic inputController;
        // private FirstVersionLockOnLogic lockOnLogic;
        private Transform camera;
        private Transform player;
        private Transform playerModel;

        private CharacterControllerConfigs configs;
        private FirstVersionControllerSettings settings => configs.firstVersionControllerSettings;

        private Vector3 horizonPlaneForward => player.forward;
        private Vector3 characterPlaneForward => playerModel.forward;

        public void Init(Player p)
        {
            configs = Main.instance.configs.ccConfigs;
            player = p.transform;
            camera = p.cameraTransform;
            playerModel = p.playerModel;
        }
        
        public IMovementLogicData RunUpdate(IInputState inputState)
            => RunUpdate((FirstVersionInputState)inputState);
        
        public FirstVersionMovementData RunUpdate(FirstVersionInputState inputState) 
            => new(GetMovementVector(inputState));

        private Vector3 GetMovementVector(FirstVersionInputState inputState)
        {
            // HandleYLockedOnMovement(firstVersionInputState.characterAxisInput);
            
            // if (lockOnLogic.lockedOn)
            //     SetModelLookAtTarget(lockOnLogic.target.position);
            
            // HandleCharacterPlaneMovement(inputState.characterPlaneInput);
            
            return GetVectorInRelationToCamRotation(inputState.characterPlaneInput) 
                   * (settings.speed * Time.deltaTime);
        }

        private void HandleCharacterPlaneMovement(Vector2 input)
        {
            Vector3 movementVector = GetVectorInRelationToCamRotation(input) * (settings.speed * Time.deltaTime);
            
            // controller logic
            player.Translate(movementVector * (settings.speed * Time.deltaTime), Space.World);
            // if (!lockOnController.lockedOn)
                SetModelLookAtTarget(player.position + movementVector);
        }
        
        private Vector3 GetVectorInRelationToCamRotation(Vector2 vector) 
            => camera.right * vector.x + camera.forward * vector.y;

        // private void HandleYLockedOnMovement(Vector2 input)
        // {
        //     if (Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * controllerData.minAngleToYRotationDeadZone) < controllerData.yRotationDeadZoneAngle)
        //         player.RotateAround(lockOnController.target.position, camController.transform.right, input.y * GetLockOnRotationSpeed() * Time.deltaTime);
        //     else
        //         HandleCharacterPlaneMovement(new Vector2(input.y, 0));
        // }
        
        private void SetModelLookAtTarget(Vector3 target) => playerModel.LookAt(target);
        
        // public void SnapPlayerToHorizonPlane() => playerModel.LookAt(transform.position + horizonPlaneForward);
        
        private float GetAngleToHorizonPlane()
        {
            float angleToHorizonPlane = Vector3.Angle(playerModel.forward, new Vector3(characterPlaneForward.x, 0, characterPlaneForward.z));
            return characterPlaneForward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
        }
    }

    public struct FirstVersionMovementData : IMovementLogicData
    {
        public Vector3 movementVector { get; set; }

        public FirstVersionMovementData(Vector3 movementVector)
        {
            this.movementVector = movementVector;
        }
    }
}