namespace UWBB.CharacterController
{
    public class StateMachine_Dodge : MultiPhaseStateMachineLogic
    {
        protected override CharacterSubState startPhase => CharacterSubState.DodgeStart;
        protected override CharacterSubState mainPhase => CharacterSubState.DodgeMain;
        protected override CharacterSubState recoveryPhase => CharacterSubState.DodgeRecovery;

        public override void EnterState()
        {
            staminaController.isWinded = false;
            base.EnterState();
        }

        public override void ProcessStateTransition() => stateMachine.characterSubState = CharacterSubState.Idle;
    }
}