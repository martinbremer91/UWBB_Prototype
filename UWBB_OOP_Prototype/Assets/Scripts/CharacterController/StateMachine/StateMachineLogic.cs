using System;

namespace UWBB.CharacterController
{
    public abstract class StateMachineLogic
    {
        protected CharacterController_StateMachine stateMachineController { get; set; }
        protected CharacterController_Animation animationController { get; set; }
        protected CharacterController_Stamina staminaController { get; set; }
        protected abstract CharacterSubState startPhase { get; }

        public virtual void Init(ICharacter character)
        {
            stateMachineController = character.GetModuleController<CharacterController_StateMachine>(ControllerType.StateMachine);
            staminaController = character.GetModuleController<CharacterController_Stamina>(ControllerType.Stamina);
            animationController = character.GetModuleController<CharacterController_Animation>(ControllerType.Animation);
        }
        
        public virtual void EnterState() => animationController.SetAnimationState(startPhase);
        public virtual void ProcessState() {}
        public virtual void ProcessStateTransition() {}
        public virtual void ExitState() {}
        public void SetAsActiveStateMachineLogic() => stateMachineController.SetActiveStateMachineLogic(this);
    }
    
    public abstract class MultiPhaseStateMachineLogic : StateMachineLogic
    {
        protected virtual CharacterSubState mainPhase => throw new ArgumentException();
        protected virtual CharacterSubState recoveryPhase => throw new ArgumentException();

        public void GoToStartPhase() => stateMachineController.characterSubState = startPhase;
        public void GoToMainPhase() => stateMachineController.characterSubState = mainPhase;
        public void GoToRecoveryPhase() => stateMachineController.characterSubState = recoveryPhase;
    }

    public abstract class ChargeableStateMachineLogic : MultiPhaseStateMachineLogic
    {
        protected abstract CharacterSubState chargePhase { get; }
        public void GoToChargePhase() => stateMachineController.characterSubState = chargePhase;
    }
}