using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class StateMachine_Walk : IStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        private StaminaActions staminaActions;
        private CharacterController_Stamina staminaCtrl;
        private InputState inputState;

        public void Init(GameManager gameManager)
        {
            staminaActions = GameConfigs.instance.staminaActions;
            stateMachine = gameManager.stateMachine;
            staminaCtrl = gameManager.staminaController;
            inputState = gameManager.inputController.inputState;
        }

        public void EnterState() { }

        public void ProcessState()
        {
            if (inputState.useItemCommand)
                stateMachine.characterSubState = CharacterSubState.UsingItemStart;
            else if (inputState.lightAttackCommand && staminaCtrl.HasStaminaForAction(staminaActions.lightAttack))
                stateMachine.characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.heavyAttackCommand && staminaCtrl.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachine.characterSubState = CharacterSubState.AttackHeavyStart;
            else if (inputState.heavyAttackChargeCommand && staminaCtrl.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachine.characterSubState = CharacterSubState.AttackHeavyCharge;
            else if (inputState.dodgeCommand && staminaCtrl.HasStaminaForAction(staminaActions.dodge))
                stateMachine.characterSubState = CharacterSubState.DodgeStart;
            else if (inputState.runCommand && !staminaCtrl.isWinded && staminaCtrl.HasStaminaForAction(staminaActions.run))
                stateMachine.characterSubState = CharacterSubState.RunStart;
            else if (inputState.moveDirection == Vector2.zero)
                stateMachine.characterSubState = CharacterSubState.Idle;
        }

        public void ExitState() { }
    }
}