using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Movement : ICharacterController
    {
        // private Transform character;
        private Transform model;
        private Transform cameraTransform;
        
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