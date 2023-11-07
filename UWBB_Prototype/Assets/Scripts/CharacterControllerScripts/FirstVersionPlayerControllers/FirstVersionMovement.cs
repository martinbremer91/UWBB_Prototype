using UnityEngine;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionMovement : MonoBehaviour
    {
        [SerializeField] private FirstVersionInputController inputController;
        [SerializeField] private FirstVersionCameraController camController;
        [SerializeField] private FirstVersionLockOnController lockOnController;
        public Transform playerModel;

        [SerializeField] private CharacterControllerConfigs configs;
        private DefaultCharacterControllerData controllerData => configs.defaultControllerData;

        private Vector3 horizonPlaneForward => transform.forward;
        private Vector3 characterPlaneForward => playerModel.forward;
        
        private void Start() => lockOnController.onLockOn += OnLockOn;
        
        private void Update()
        {
            FirstVersionInputState firstVersionInputState = inputController.FirstVersionInputState;
            HandleCharacterPlaneMovement(firstVersionInputState.characterPlaneInput);
            HandleYLockedOnMovement(firstVersionInputState.characterAxisInput);
            
            if (lockOnController.lockedOn)
                SetModelLookAtTarget(lockOnController.target.position);
        }

        private void HandleCharacterPlaneMovement(Vector2 input)
        {
            Vector3 movementVector = camController.GetVectorInRelationToCamRotation(input);
            transform.Translate(movementVector * (controllerData.speed * Time.deltaTime), Space.World);
            
            if (!lockOnController.lockedOn)
                SetModelLookAtTarget(transform.position + movementVector);
        }
        
        private void HandleYLockedOnMovement(Vector2 input)
        {
            if (Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * controllerData.minAngleToYRotationDeadZone) < controllerData.yRotationDeadZoneAngle)
                transform.RotateAround(lockOnController.target.position, camController.transform.right, input.y * GetLockOnRotationSpeed() * Time.deltaTime);
            else
                HandleCharacterPlaneMovement(new Vector2(input.y, 0));
        }
        
        private void SetModelLookAtTarget(Vector3 target) => playerModel.LookAt(target);
        
        public void SnapPlayerToHorizonPlane() => playerModel.LookAt(transform.position + horizonPlaneForward);
        
        private float GetAngleToHorizonPlane()
        {
            float angleToHorizonPlane = Vector3.Angle(playerModel.forward, new Vector3(characterPlaneForward.x, 0, characterPlaneForward.z));
            return characterPlaneForward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
        }
        
        private float GetLockOnRotationSpeed()
        {
            Vector3 center = lockOnController.target.position;
            float circumference = (center - transform.position).magnitude * Mathf.PI * 2;
        
            return (controllerData.speed / circumference) * 360;
        }

        private void OnLockOn()
        {
            SnapPlayerToHorizonPlane();
            playerModel.LookAt(lockOnController.target.position);
        }

        private void OnDestroy() => lockOnController.onLockOn -= OnLockOn;
    }
}