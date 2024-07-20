using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class StateMachine_Run : MultiPhaseStateMachineLogic
    {
        private InputState inputState;
        private StaminaActions staminaActions;

        protected override CharacterSubState mainPhase => CharacterSubState.RunMain;

        private float minimumRunTimerForRunningAttack;
        
        public override void Init(ICharacter character)
        {
            base.Init(character);
            staminaActions = GameConfigs.instance.staminaActions;
            inputState = character.GetModuleController<CharacterController_Input>(ControllerType.Input).inputState;
            minimumRunTimerForRunningAttack = character.characterConfigs.minimumRunTimerForRunningAttack;
        }

        public override void EnterState(CharacterSubState subState)
        {
            stateMachineController.currentRunActionTimer = 0;
            base.EnterState(subState);
        }

        public override void ProcessState()
        {
            if (inputState.useItemCommand)
                stateMachineController.characterSubState = CharacterSubState.UseItemStart;
            else if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
                stateMachineController.characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.heavyAttackCommand && staminaController.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachineController.characterSubState = CharacterSubState.AttackHeavyStart;
            else if (inputState.moveDirection == Vector2.zero)
                stateMachineController.characterSubState = CharacterSubState.Idle;
            else if (!inputState.runCommand)
                stateMachineController.characterSubState = CharacterSubState.Walk;
            else if (stateMachineController.characterSubState is CharacterSubState.RunStart)
                stateMachineController.currentRunActionTimer += Time.deltaTime;

            if (stateMachineController.currentRunActionTimer >= minimumRunTimerForRunningAttack)
                stateMachineController.characterSubState = CharacterSubState.RunMain;
        }
    }
}