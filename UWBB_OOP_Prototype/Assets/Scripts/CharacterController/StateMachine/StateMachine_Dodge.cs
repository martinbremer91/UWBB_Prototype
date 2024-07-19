using UnityEngine;

namespace UWBB.CharacterController
{
    public class StateMachine_Dodge : IMultiPhaseStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        public CharacterController_Animation animationController { get; set; }
        private CharacterController_Stamina staminaCtrl;
        
        public CharacterSubState startPhase => CharacterSubState.DodgeStart;
        public CharacterSubState mainPhase => CharacterSubState.DodgeMain;
        public CharacterSubState recoveryPhase => CharacterSubState.DodgeRecovery;
        
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
            // animationController.animator.Play(animationStateID);
        }

        public void ProcessState()
        {
            // eventually: buffer dodge attack
        }

        public void ProcessStateTransition()
        {
            stateMachine.characterSubState = CharacterSubState.Idle;
        }

        public void ExitState() { }
    }
}