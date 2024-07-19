using UnityEngine;

namespace UWBB.CharacterController
{
    public static class AnimationConstants
    {
        public static readonly int idle = Animator.StringToHash("Idle");
        public static readonly int walk = Animator.StringToHash("Walk");
        public static readonly int runStart = Animator.StringToHash("Run");
        public static readonly int dodgeStart = Animator.StringToHash("Dodge_Start");
        public static readonly int attackLightStart = Animator.StringToHash("LightAttack_Start");
        public static readonly int attackHeavyStart = Animator.StringToHash("HeavyAttack_Start");
        public static readonly int useItemStart = Animator.StringToHash("UseItem_Start");
        // public static readonly int stunMain = Animator.StringToHash("");
    }
}