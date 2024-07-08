using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Movement
    {
        private readonly Transform character;
        private readonly Transform model;
        private readonly Transform cameraTransform;

        private const float moveSpeed = 10;
        
        public CharacterController_Movement(Transform character, Transform model, Transform cameraTransform)
        {
            this.character = character;
            this.model = model;
            this.cameraTransform = cameraTransform;
        }

        public void Update(InputState input)
        {
            if (input.moveDirection == Vector2.zero)
                return;

            Vector2 scaledMoveDirection = input.moveDirection * (Time.deltaTime * moveSpeed);
            float xMove = scaledMoveDirection.x;
            float yMove = scaledMoveDirection.y;
            
            Vector3 camLevelledForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
            Vector3 camLevelledRight = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z).normalized;
            Vector3 moveDelta = camLevelledForward * yMove + camLevelledRight * xMove;

            character.position += moveDelta;
            model.LookAt(model.position + moveDelta.normalized);
        }
    }
}