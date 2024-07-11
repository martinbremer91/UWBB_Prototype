using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Movement
    {
        private readonly Transform character;
        private readonly Transform model;
        private readonly Transform cameraTransform;

        private const float moveSpeed = 10;
        private const float runSpeed = 25;
        private const float dodgeSpeed = 50;
        
        public CharacterController_Movement(Transform character, Transform model, Transform cameraTransform)
        {
            this.character = character;
            this.model = model;
            this.cameraTransform = cameraTransform;
        }

        public void Update(InputState input, MoveSpeedState moveSpeedState)
        {
            
        }
    }
    
    public enum MoveSpeedState
    {
        Walk,
        Run,
        Dodge,
        Slowed,
        Immobile,
    }
}