using UnityEngine.InputSystem;

namespace UWBB.CharacterController
{
    public class StateMachine_Stunned : IStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        private CharacterController_Stamina staminaCtrl;

        public void Init(GameManager gameManager)
        {
            stateMachine = gameManager.stateMachine;
            staminaCtrl = gameManager.staminaController;
        }

        public void EnterState()
        {
            staminaCtrl.isWinded = false;
        }

        public void ProcessState()
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
                stateMachine.characterSubState = CharacterSubState.Idle;
        }

        public void ExitState()
        {
        }
    }
}