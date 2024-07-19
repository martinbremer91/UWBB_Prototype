using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class StateMachine_Walk : StateMachineLogic
    {
        private StaminaActions staminaActions;
        private InputState inputState;

        protected override CharacterSubState startPhase => CharacterSubState.Walk;

        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);
            staminaActions = GameConfigs.instance.staminaActions;
            inputState = gameManager.inputController.inputState;
        }

        public override void ProcessState()
        {
            if (inputState.useItemCommand)
                stateMachine.characterSubState = CharacterSubState.UseItemStart;
            else if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
                stateMachine.characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.heavyAttackCommand && staminaController.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachine.characterSubState = CharacterSubState.AttackHeavyStart;
            else if (inputState.dodgeCommand && staminaController.HasStaminaForAction(staminaActions.dodge))
                stateMachine.characterSubState = CharacterSubState.DodgeStart;
            else if (inputState.runCommand && !staminaController.isWinded && staminaController.HasStaminaForAction(staminaActions.run))
                stateMachine.characterSubState = CharacterSubState.RunStart;
            else if (inputState.moveDirection == Vector2.zero)
                stateMachine.characterSubState = CharacterSubState.Idle;
        }
    }
}