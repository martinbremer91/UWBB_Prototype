using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class StateMachine_Run : MultiPhaseStateMachineLogic
    {
        private InputState inputState;
        private StaminaActions staminaActions;

        protected override CharacterSubState startPhase => CharacterSubState.RunStart;
        protected override CharacterSubState mainPhase => CharacterSubState.RunMain;

        private float minimumRunTimerForRunningAttack;
        
        public override void Init(ICharacter character)
        {
            base.Init(character);
            staminaActions = GameConfigs.instance.staminaActions;
            inputState = character.GetModuleController<CharacterController_Input>(ControllerType.Input).inputState;
            minimumRunTimerForRunningAttack = character.characterConfigs.minimumRunTimerForRunningAttack;
        }

        public override void EnterState()
        {
            stateMachine.currentRunActionTimer = 0;
            base.EnterState();
        }

        public override void ProcessState()
        {
            if (inputState.useItemCommand)
                stateMachine.characterSubState = CharacterSubState.UseItemStart;
            else if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
                stateMachine.characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.heavyAttackCommand && staminaController.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachine.characterSubState = CharacterSubState.AttackHeavyStart;
            else if (inputState.moveDirection == Vector2.zero)
                stateMachine.characterSubState = CharacterSubState.Idle;
            else if (!inputState.runCommand)
                stateMachine.characterSubState = CharacterSubState.Walk;
            else if (stateMachine.characterSubState is CharacterSubState.RunStart)
                stateMachine.currentRunActionTimer += Time.deltaTime;

            if (stateMachine.currentRunActionTimer >= minimumRunTimerForRunningAttack)
                stateMachine.characterSubState = CharacterSubState.RunMain;
        }
    }
}