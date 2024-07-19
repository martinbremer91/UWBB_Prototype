using UnityEngine;
using UnityEngine.InputSystem;

namespace UWBB.CharacterController
{
    public class StateMachine_UsingItem : IMultiPhaseStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        public CharacterController_Animation animationController { get; set; }
        private CharacterController_Stamina staminaCtrl;

        public CharacterSubState startPhase => CharacterSubState.UseItemStart;
        public CharacterSubState mainPhase => CharacterSubState.UseItemStart;
        public CharacterSubState recoveryPhase => CharacterSubState.UseItemRecovery;
        
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
            if (Keyboard.current.iKey.wasPressedThisFrame)
                stateMachine.characterSubState = CharacterSubState.Idle;
            // might be unnecessary? state can only elapse or be interrupted by external factors
            // maybe set move speed to "slowed"
        }

        public void ProcessStateTransition()
        {
            stateMachine.characterSubState = CharacterSubState.Idle;
        }

        public void ExitState() { }
    }
}