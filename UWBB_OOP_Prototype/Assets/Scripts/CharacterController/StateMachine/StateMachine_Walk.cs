using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class StateMachine_Walk : StateMachineLogic
    {
        private StaminaActions staminaActions;
        private InputState inputState;

        protected override CharacterSubState startPhase => CharacterSubState.Walk;

        public override void Init(ICharacter character)
        {
            base.Init(character);
            staminaActions = GameConfigs.instance.staminaActions;
            inputState = character.GetModuleController<CharacterController_Input>(ControllerType.Input).inputState;
        }

        public override void ProcessState()
        {
            if (inputState.useItemCommand)
                stateMachineController.characterSubState = CharacterSubState.UseItemStart;
            else if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
                stateMachineController.characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.heavyAttackCommand && staminaController.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachineController.characterSubState = CharacterSubState.AttackHeavyStart;
            else if (inputState.dodgeCommand && staminaController.HasStaminaForAction(staminaActions.dodge))
                stateMachineController.characterSubState = CharacterSubState.DodgeStart;
            else if (inputState.runCommand && !staminaController.isWinded && staminaController.HasStaminaForAction(staminaActions.run))
                stateMachineController.characterSubState = CharacterSubState.RunStart;
            else if (inputState.moveDirection == Vector2.zero)
                stateMachineController.characterSubState = CharacterSubState.Idle;
        }
    }
}