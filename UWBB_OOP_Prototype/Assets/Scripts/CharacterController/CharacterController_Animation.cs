using System;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Animation
    {
        public Animator animator;

        public void Init(Character_Player character)
        {
            animator = character.GetComponent<Animator>();

            foreach (var start in animator.GetBehaviours<StartSubStatePhase>())
                start.Init(character.characterStatePhaseController);
            foreach (var main in animator.GetBehaviours<MainSubStatePhase>())
                main.Init(character.characterStatePhaseController);
            foreach (var recovery in animator.GetBehaviours<RecoverySubStatePhase>())
                recovery.Init(character.characterStatePhaseController);

            foreach (var preCharge in animator.GetBehaviours<PreChargePhase>())
                preCharge.Init(character);
            foreach (var charge in animator.GetBehaviours<ChargePhase>())
                charge.Init(character);
        }

        public void SetAnimationState(CharacterSubState characterSubState)
            => animator.Play(GetAnimatorStateHashFrom(characterSubState));

        private int GetAnimatorStateHashFrom(CharacterSubState characterSubState)
        {
            return characterSubState switch
            {
                CharacterSubState.Idle => AnimationConstants.idle,
                CharacterSubState.Walk => AnimationConstants.walk,
                CharacterSubState.RunStart => AnimationConstants.runStart,
                CharacterSubState.DodgeStart => AnimationConstants.dodgeStart,
                CharacterSubState.AttackLightStart => AnimationConstants.attackLightStart,
                CharacterSubState.AttackHeavyStart => AnimationConstants.attackHeavyStart,
                CharacterSubState.UseItemStart => AnimationConstants.useItemStart,
                // CharacterSubState.StunMain => AnimationConstants.,
                _ => throw new ArgumentOutOfRangeException(nameof(characterSubState), characterSubState, null)
            };
        }
    }
}