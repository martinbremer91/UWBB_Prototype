using UnityEngine;

namespace UWBB.CharacterController
{
    public static class AnimationConstants
    {
        // STATES
        public static readonly int idle = Animator.StringToHash("Idle");
        public static readonly int walk = Animator.StringToHash("Walk");
        public static readonly int runStart = Animator.StringToHash("Run");
        public static readonly int dodgeStart = Animator.StringToHash("Dodge_Start");
        public static readonly int attackLightStart = Animator.StringToHash("LightAttack_Start");
        public static readonly int attackHeavyStart = Animator.StringToHash("HeavyAttack_Start");
        public static readonly int useItemStart = Animator.StringToHash("UseItem_Start");
        public static readonly int stunFlinch = Animator.StringToHash("Stun_Flinch");
        public static readonly int stunStagger = Animator.StringToHash("Stun_Stagger");
        public static readonly int stunKnockdown = Animator.StringToHash("Stun_Knockdown");
        
        // TRIGGERS
        public static readonly int chargeRelease = Animator.StringToHash("ChargeRelease");
        
        // BOOLS
        public static readonly int charge = Animator.StringToHash("Charge");
    }
}