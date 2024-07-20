using System;

namespace UWBB.CharacterController
{
    public abstract class StateMachineLogic
    {
        protected CharacterController_StateMachine stateMachine { get; set; }
        protected CharacterStatePhaseController characterStatePhaseController { get; set; }
        protected CharacterController_Animation animationController { get; set; }
        protected CharacterController_Stamina staminaController { get; set; }
        protected abstract CharacterSubState startPhase { get; }

        public virtual void Init(Character_Player character)
        {
            stateMachine = character.stateMachineController;
            characterStatePhaseController = character.characterStatePhaseController;
            staminaController = character.staminaController;
            animationController = character.animationController;
        }
        
        public virtual void EnterState() => animationController.SetAnimationState(startPhase);
        public virtual void ProcessState() {}
        public virtual void ProcessStateTransition() {}
        public virtual void ExitState() {}
        public void SetAsActiveStateMachineLogic() => characterStatePhaseController.SetActiveStateMachineLogic(this);
    }
    
    public abstract class MultiPhaseStateMachineLogic : StateMachineLogic
    {
        protected virtual CharacterSubState mainPhase => throw new ArgumentException();
        protected virtual CharacterSubState recoveryPhase => throw new ArgumentException();

        public void GoToStartPhase() => stateMachine.characterSubState = startPhase;
        public void GoToMainPhase() => stateMachine.characterSubState = mainPhase;
        public void GoToRecoveryPhase() => stateMachine.characterSubState = recoveryPhase;
    }

    public abstract class ChargeableStateMachineLogic : MultiPhaseStateMachineLogic
    {
        protected abstract CharacterSubState chargePhase { get; }
        public void GoToChargePhase() => stateMachine.characterSubState = chargePhase;
    }
}