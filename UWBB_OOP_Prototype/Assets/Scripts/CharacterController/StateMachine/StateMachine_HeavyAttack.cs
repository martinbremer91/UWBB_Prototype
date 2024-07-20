namespace UWBB.CharacterController
{
    public class StateMachine_HeavyAttack : ChargeableStateMachineLogic
    {
        protected override CharacterSubState chargePhase => CharacterSubState.AttackHeavyCharge;
        protected override CharacterSubState mainPhase => CharacterSubState.AttackHeavyMain;
        protected override CharacterSubState recoveryPhase => CharacterSubState.AttackHeavyRecovery;

        public override void EnterState(CharacterSubState subState)
        {
            staminaController.isWinded = false;
            base.EnterState(subState);
        }

        public override void ProcessStateTransition() => stateMachineController.characterSubState = CharacterSubState.Idle;
    }
}