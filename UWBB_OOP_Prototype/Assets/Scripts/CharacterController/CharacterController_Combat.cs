using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Combat
    {
        private GameObject weapon;
        private CharacterController_Stamina staminaController;
        
        private const float attackDuration = 1;
        private const float dodgeDuration = 1;
        private const float runStartDuration = 1;

        private float timer;
        
        public CharacterController_Combat(GameObject weapon, CharacterController_Stamina staminaController)
        {
            this.weapon = weapon;
            this.staminaController = staminaController;
        }
        
        public void Update(InputState input)
        {
            
        }
    }
}