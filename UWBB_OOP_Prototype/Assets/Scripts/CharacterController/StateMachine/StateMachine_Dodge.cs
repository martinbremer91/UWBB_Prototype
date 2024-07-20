namespace UWBB.CharacterController
{
    public class StateMachine_Dodge : MultiPhaseStateMachineLogic
    {
        protected override CharacterSubState mainPhase => CharacterSubState.DodgeMain;
        protected override CharacterSubState recoveryPhase => CharacterSubState.DodgeRecovery;

        public override void EnterState(CharacterSubState subState)
        {
            staminaController.isWinded = false;
            base.EnterState(subState);
        }

        public override void ProcessStateTransition() => stateMachineController.characterSubState = CharacterSubState.Idle;
    }
}