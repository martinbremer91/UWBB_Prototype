using UnityEngine;
using UWBB.GameFramework;

namespace UWBB.CharacterController
{
    public class PlayerMovementLogic 
    {
        private PlayerInputLogic inputController;
        // private FirstVersionLockOnLogic lockOnLogic;
        private Transform camera;
        private Transform player;
        private Transform playerModel;

        private CharacterControllerConfigs configs;

        private Vector3 horizonPlaneForward => player.forward;
        private Vector3 characterPlaneForward => playerModel.forward;

        public void Init(Player p)
        {
            configs = Main.instance.configs.ccConfigs;
            player = p.transform;
            camera = p.cameraTransform;
            playerModel = p.playerModel;
        }
        
        public PlayerMovementData RunUpdate(InputState inputState) 
            => new(GetMovementVector(inputState), inputState.dashCommand);
        
        private Vector3 GetMovementVector(InputState inputState)
        {
            // HandleYLockedOnMovement(firstVersionInputState.characterAxisInput);
            
            // if (lockOnLogic.lockedOn)
            //     SetModelLookAtTarget(lockOnLogic.target.position);
            
            // HandleCharacterPlaneMovement(inputState.characterPlaneInput);

            Vector3 cameraPlaneMovementVector = GetVectorInRelationToCamRotation(inputState.characterPlaneInput);
            Vector3 finalMovementVector =
                AddWorldYInputVectorToCameraPlaneMovementVector(inputState, cameraPlaneMovementVector);
            return finalMovementVector;
        }

        private void HandleCharacterPlaneMovement(Vector2 input)
        {
            Vector3 movementVector = GetVectorInRelationToCamRotation(input) * (configs.speed * Time.deltaTime);
            
            // controller logic
            player.Translate(movementVector * (configs.speed * Time.deltaTime), Space.World);
            // if (!lockOnController.lockedOn)
                SetModelLookAtTarget(player.position + movementVector);
        }
        
        private Vector3 GetVectorInRelationToCamRotation(Vector2 vector) 
            => camera.right * vector.x + camera.forward * vector.y;

        private Vector3 AddWorldYInputVectorToCameraPlaneMovementVector(InputState inputState, Vector3 camPlaneMove)
            => inputState.worldYInput == 0 ? camPlaneMove : Vector3.up * inputState.worldYInput;

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

    public struct PlayerMovementData
    {
        public Vector3 movementVector { get; set; }
        public bool dashCommand { get; set; }

        public PlayerMovementData(Vector3 movementVector, bool dashCommand)
        {
            this.movementVector = movementVector;
            this.dashCommand = dashCommand;
        }
    }
}