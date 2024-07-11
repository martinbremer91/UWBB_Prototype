using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Combat
    {
        private GameObject weapon;
        
        private const float attackDuration = 1;
        private const float dodgeDuration = 1;
        private const float runStartDuration = 1;

        private float timer;
        
        public CharacterController_Combat(GameObject weapon)
        {
            this.weapon = weapon;
        }
        
        public void Update(InputState input)
        {
            
        }
    }

    public enum AttackState
    {
        ReadyStandard,
        ReadyCombo,
        ReadyDodgeAttack,
        ReadyRunningAttack,
        ReadyBackStepAttack,
        NonActionable,
    }
}