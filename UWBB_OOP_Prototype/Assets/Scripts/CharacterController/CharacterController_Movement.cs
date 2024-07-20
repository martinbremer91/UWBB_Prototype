using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Movement : ICharacterController
    {
        private Transform character;
        private Transform model;
        private Transform cameraTransform;

        // TODO: move this elsewhere
        private const float moveSpeed = 10;
        private const float runSpeed = 25;
        private const float dodgeSpeed = 50;
        
        public void Init(ICharacter character)
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