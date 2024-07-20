namespace UWBB.CharacterController
{
    public class StateMachine_LightAttack : MultiPhaseStateMachineLogic
    {
        protected override CharacterSubState mainPhase => CharacterSubState.AttackLightMain;
        protected override CharacterSubState recoveryPhase => CharacterSubState.AttackLightRecovery;

        public override void EnterState(CharacterSubState subState)
        {
            staminaController.isWinded = false;
            base.EnterState(subState);
        }

        public override void ProcessStateTransition() => stateMachineController.characterSubState = CharacterSubState.Idle;
    }
}