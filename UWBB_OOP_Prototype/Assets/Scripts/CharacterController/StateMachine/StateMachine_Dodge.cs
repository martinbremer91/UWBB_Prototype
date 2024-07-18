using UnityEngine;

namespace UWBB.CharacterController
{
    public class StateMachine_Dodge : IThreePhaseStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        private CharacterController_Stamina staminaCtrl;
        private CharacterController_Animation animationCtrl;
        private static readonly int Placeholder = Animator.StringToHash("Placeholder");

        public CharacterSubState mainPhase => CharacterSubState.DodgeMain;
        public CharacterSubState recoveryPhase => CharacterSubState.DodgeRecovery;
        
        public void Init(GameManager gameManager)
        {
            stateMachine = gameManager.stateMachine;
            characterStatePhaseController = gameManager.characterStatePhaseController;
            staminaCtrl = gameManager.staminaController;
            animationCtrl = gameManager.animationController;
        }

        public void EnterState()
        {
            staminaCtrl.isWinded = false;
            characterStatePhaseController.threePhaseStateMachineLogic = this;
            animationCtrl.animator.SetTrigger(Placeholder);
        }

        public void ProcessState()
        {
            // eventually: buffer dodge attack
        }

        public void ExitState()
        {
            
        }
    }
}