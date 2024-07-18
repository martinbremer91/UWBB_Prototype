using UnityEngine.InputSystem;

namespace UWBB.CharacterController
{
    public class StateMachine_HeavyAttack : IThreePhaseStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        private CharacterController_Stamina staminaCtrl;

        public CharacterSubState mainPhase => CharacterSubState.AttackHeavyMain;
        public CharacterSubState recoveryPhase => CharacterSubState.AttackHeavyRecovery;
        
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
            // eventually: buffer combo attack
        }

        public void ExitState()
        {
        }
    }
}