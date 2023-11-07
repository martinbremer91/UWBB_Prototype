using UnityEngine;
using UnityEngine.Serialization;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionCameraController : MonoBehaviour
    {
        private FirstVersionInputController inputController;
        [SerializeField] private FirstVersionLockOnController lockOnController;
        [SerializeField] private FirstVersionMovement firstVersionMovement;
        [SerializeField] private Transform player;

        [SerializeField] private float rotationSpeed = 180;

        private void Awake() => lockOnController.onLockOn += OnLockOn;

        private void LateUpdate()
        {
            if (inputController.FirstVersionInputState.snapCommand)
            {
                SnapCamToHorizonPlane();
                inputController.FirstVersionInputState.snapCommand = false;
            } else if (!lockOnController.lockedOn)
                HandleCamInput(inputController.FirstVersionInputState.characterAxisInput);
            else
                OnLockOn();
        }

        private void HandleCamInput(Vector2 input)
        {
            transform.RotateAround(player.position, Vector3.up, input.x * (rotationSpeed * Time.deltaTime));

            if (Mathf.Abs(input.y) >= .75f && Mathf.Abs(GetAngleToHorizonPlane() - Mathf.Sign(input.y) * 10) < 88)
                transform.RotateAround(player.position, transform.right, input.y * (rotationSpeed * Time.deltaTime));
        }

        public Vector3 GetVectorInRelationToCamRotation(Vector2 vector)
        {
            var tf = transform;
            return tf.right * vector.x + tf.forward * vector.y;
        }

        private void SnapCamToHorizonPlane()
        {
            float angleToHorizonPlane = GetAngleToHorizonPlane();
            transform.RotateAround(player.position, transform.right, angleToHorizonPlane);
            firstVersionMovement.SnapPlayerToHorizonPlane();
        }
        
        private float GetAngleToHorizonPlane()
        {
            float angleToHorizonPlane = Vector3.Angle(transform.forward, new Vector3(transform.forward.x, 0, transform.forward.z));
            return transform.forward.y < 0 ? -angleToHorizonPlane : angleToHorizonPlane;
        }

        private void OnLockOn()
        {
            Vector3 targetPlayerDirection = (player.position - lockOnController.target.position).normalized;
            float distanceToPlayer = (player.position - transform.position).magnitude;
            transform.position = player.position + targetPlayerDirection * distanceToPlayer;
            transform.LookAt(lockOnController.target.position);
        }

        private void OnDestroy()
        {
            lockOnController.onLockOn -= OnLockOn;
        }
    }
}
