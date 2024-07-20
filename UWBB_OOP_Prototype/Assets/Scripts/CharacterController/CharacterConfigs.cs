using UnityEngine;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterConfigs_", menuName = "UWBB/CharacterConfigs")]
    public class CharacterConfigs : ScriptableObject
    {
        public float minimumRunTimerForRunningAttack = 1f;
        
        public float moveSpeed = 10;
        public float runSpeed = 25;
        public float dodgeSpeed = 50;
        
        public float staminaTotal = 100f;
        public float regenRate = 20f;
        public float staminaRegenBlockDuration = .5f;
    }
}