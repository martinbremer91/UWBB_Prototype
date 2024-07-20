namespace UWBB.CharacterController
{
    public class StateMachine_UsingItem : MultiPhaseStateMachineLogic
    {
        protected override CharacterSubState mainPhase => CharacterSubState.UseItemStart;
        protected override CharacterSubState recoveryPhase => CharacterSubState.UseItemRecovery;

        public override void EnterState(CharacterSubState subState)
        {
            staminaController.isWinded = false;
            base.EnterState(subState);
        }

        public override void ProcessStateTransition() => stateMachineController.characterSubState = CharacterSubState.Idle;
    }
}