using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Combat : ICharacterController
    {
        private GameObject weapon;
        private CharacterController_Stamina staminaController;
        
        private const float attackDuration = 1;
        private const float dodgeDuration = 1;
        private const float runStartDuration = 1;

        private float timer;
        
        public void Init(ICharacter character)
        {
            
        }
    }
}