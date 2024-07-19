using UnityEngine;

namespace UWBB.CharacterController
{
    public class StateMachine_HeavyAttack : IChargeableStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        public CharacterController_Animation animationController { get; set; }
        private CharacterController_Stamina staminaCtrl;
        
        public CharacterSubState startPhase => CharacterSubState.AttackHeavyStart;
        public CharacterSubState chargePhase => CharacterSubState.AttackHeavyCharge;
        public CharacterSubState mainPhase => CharacterSubState.AttackHeavyMain;
        public CharacterSubState recoveryPhase => CharacterSubState.AttackHeavyRecovery;

        private bool chargedAttack;
        
        public void Init(GameManager gameManager)
        {
            stateMachine = gameManager.stateMachine;
            characterStatePhaseController = gameManager.characterStatePhaseController;
            staminaCtrl = gameManager.staminaController;
            animationController = gameManager.animationController;
        }

        public void EnterState()
        {
            staminaCtrl.isWinded = false;
            chargedAttack = stateMachine.characterSubState is CharacterSubState.AttackHeavyCharge;
            // animationController.animator.Play(animationStateID);
        }

        public void ProcessState()
        {
        }

        public void ProcessStateTransition()
        {
            stateMachine.characterSubState = CharacterSubState.Idle;
        }

        public void ExitState()
        {
        }
    }
}