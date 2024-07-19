namespace UWBB.CharacterController
{
    public class StateMachine_UsingItem : MultiPhaseStateMachineLogic
    {
        protected override CharacterSubState startPhase => CharacterSubState.UseItemStart;
        protected override CharacterSubState mainPhase => CharacterSubState.UseItemStart;
        protected override CharacterSubState recoveryPhase => CharacterSubState.UseItemRecovery;

        public override void EnterState()
        {
            staminaController.isWinded = false;
            base.EnterState();
        }

        public override void ProcessStateTransition() => stateMachine.characterSubState = CharacterSubState.Idle;
    }
}