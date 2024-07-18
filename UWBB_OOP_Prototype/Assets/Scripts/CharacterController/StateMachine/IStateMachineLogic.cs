namespace UWBB.CharacterController
{
    public interface IStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        public CharacterController_Animation animationController { get; set; }
        public void Init(GameManager gameManager);
        public void EnterState();
        public void ProcessState();
        public void ProcessStateTransition();
        public void ExitState();
        public void SetAsActiveStateMachineLogic() => characterStatePhaseController.SetActiveStateMachineLogic(this);
    }
    
    public interface IMultiPhaseStateMachineLogic : IStateMachineLogic
    {
        public CharacterSubState startPhase { get; }
        public CharacterSubState mainPhase { get; }
        public CharacterSubState recoveryPhase { get; }

        public void GoToStartPhase() => stateMachine.characterSubState = startPhase;
        public void GoToMainPhase() => stateMachine.characterSubState = mainPhase;
        public void GoToRecoveryPhase() => stateMachine.characterSubState = recoveryPhase;
    }

    public interface IChargeableStateMachineLogic : IMultiPhaseStateMachineLogic
    {
        public CharacterSubState chargePhase { get; }
        public void GoToChargePhase() => stateMachine.characterSubState = chargePhase;
    }
}