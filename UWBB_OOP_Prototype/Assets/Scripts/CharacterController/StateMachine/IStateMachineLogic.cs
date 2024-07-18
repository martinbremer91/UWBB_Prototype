namespace UWBB.CharacterController
{
    public interface IStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public void Init(GameManager gameManager);
        public void EnterState();
        public void ProcessState();
        public void ExitState();
    }
    
    public interface IThreePhaseStateMachineLogic : IStateMachineLogic
    {
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        
        public CharacterSubState mainPhase { get; }
        public CharacterSubState recoveryPhase { get; }

        public void AdvanceToMainPhase() => stateMachine.characterSubState = mainPhase;
        public void AdvanceToRecoveryPhase() => stateMachine.characterSubState = recoveryPhase;
    }
}