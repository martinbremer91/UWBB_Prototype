using UnityEngine.InputSystem;

namespace UWBB.CharacterController
{
    public class StateMachine_UsingItem : IThreePhaseStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        private CharacterController_Stamina staminaCtrl;

        public CharacterSubState mainPhase => CharacterSubState.UsingItemStart;
        public CharacterSubState recoveryPhase => CharacterSubState.UsingItemRecovery;
        
        public void Init(GameManager gameManager)
        {
            stateMachine = gameManager.stateMachine;
            characterStatePhaseController = gameManager.characterStatePhaseController;
            staminaCtrl = gameManager.staminaController;
        }

        public void EnterState()
        {
            staminaCtrl.isWinded = false;
            characterStatePhaseController.threePhaseStateMachineLogic = this;
        }

        public void ProcessState()
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
                stateMachine.characterSubState = CharacterSubState.Idle;
            // might be unnecessary? state can only elapse or be interrupted by external factors
            // maybe set move speed to "slowed"
        }

        public void ExitState() { }
    }
}