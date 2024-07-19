namespace UWBB.CharacterController
{
    public class StateMachine_HeavyAttack : ChargeableStateMachineLogic
    {
        protected override CharacterSubState startPhase => CharacterSubState.AttackHeavyStart;
        protected override CharacterSubState chargePhase => CharacterSubState.AttackHeavyCharge;
        protected override CharacterSubState mainPhase => CharacterSubState.AttackHeavyMain;
        protected override CharacterSubState recoveryPhase => CharacterSubState.AttackHeavyRecovery;

        public override void EnterState()
        {
            staminaController.isWinded = false;
            base.EnterState();
        }

        public override void ProcessStateTransition() => stateMachine.characterSubState = CharacterSubState.Idle;
    }
}