using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class StateMachine_Idle : StateMachineLogic
    {
        private StaminaActions staminaActions;
        private InputState inputState;

        protected override CharacterSubState startPhase => CharacterSubState.Idle;

        public override void Init(Character_Player character)
        {
            base.Init(character);
            staminaActions = GameConfigs.instance.staminaActions;
            inputState = character.inputController.inputState;
        }

        public override void ProcessState()
        {
            if (inputState.useItemCommand)
                stateMachine.characterSubState = CharacterSubState.UseItemStart;
            else if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
                stateMachine.characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.heavyAttackCommand && staminaController.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachine.characterSubState = CharacterSubState.AttackHeavyStart;
            else if (inputState.moveDirection != Vector2.zero)
                stateMachine.characterSubState = CharacterSubState.Walk;
            else if (inputState.dodgeCommand && staminaController.HasStaminaForAction(staminaActions.dodge))
                stateMachine.characterSubState = CharacterSubState.DodgeStart;
        }
    }
}