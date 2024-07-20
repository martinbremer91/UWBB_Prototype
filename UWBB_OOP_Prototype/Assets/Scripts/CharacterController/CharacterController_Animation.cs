using System;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Animation : ICharacterController
    {
        private Animator animator;

        public void Init(ICharacter character)
        {
            animator = character.monoBehaviour.GetComponent<Animator>();
            CharacterController_StateMachine stateMachine =
                character.GetModuleController<CharacterController_StateMachine>(ControllerType.StateMachine);
            
            foreach (var main in animator.GetBehaviours<MainPhaseState>())
                main.Init(stateMachine);
            foreach (var recovery in animator.GetBehaviours<RecoveryPhaseState>())
                recovery.Init(stateMachine);
            foreach (var single in animator.GetBehaviours<SinglePhaseState>())
                single.Init(stateMachine);
            
            foreach (var preCharge in animator.GetBehaviours<PreChargeState>())
                preCharge.Init(character);
            foreach (var charge in animator.GetBehaviours<ChargeState>())
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
                CharacterSubState.StunFlinch => AnimationConstants.stunFlinch,
                CharacterSubState.StunStagger => AnimationConstants.stunStagger,
                CharacterSubState.StunKnockdown => AnimationConstants.stunKnockdown,
                _ => throw new ArgumentOutOfRangeException(nameof(characterSubState), characterSubState, null)
            };
        }
    }
}