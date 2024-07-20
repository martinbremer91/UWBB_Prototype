namespace UWBB.CharacterController
{
    public class StateMachine_LightAttack : MultiPhaseStateMachineLogic
    {
        protected override CharacterSubState startPhase => CharacterSubState.AttackLightStart;
        protected override CharacterSubState mainPhase => CharacterSubState.AttackLightMain;
        protected override CharacterSubState recoveryPhase => CharacterSubState.AttackLightRecovery;

        public override void EnterState()
        {
            staminaController.isWinded = false;
            base.EnterState();
        }

        public override void ProcessStateTransition() => stateMachineController.characterSubState = CharacterSubState.Idle;
    }
}