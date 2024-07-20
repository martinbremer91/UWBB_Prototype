using System;

namespace UWBB.CharacterController
{
    public abstract class StateMachineLogic
    {
        protected CharacterController_StateMachine stateMachineController { get; set; }
        protected CharacterController_Animation animationController { get; set; }
        protected CharacterController_Stamina staminaController { get; set; }

        public virtual void Init(ICharacter character)
        {
            stateMachineController = character.GetModuleController<CharacterController_StateMachine>(ControllerType.StateMachine);
            staminaController = character.GetModuleController<CharacterController_Stamina>(ControllerType.Stamina);
            animationController = character.GetModuleController<CharacterController_Animation>(ControllerType.Animation);
        }
        
        public virtual void EnterState(CharacterSubState subState) => animationController.SetAnimationState(subState);
        public virtual void ProcessState() {}
        public virtual void ExitState() {}
        public virtual void SetAsActiveStateMachineLogic() => stateMachineController.SetActiveStateMachineLogic(this);
        public virtual void ProcessStateTransition() {}
    }
    
    public abstract class MultiPhaseStateMachineLogic : StateMachineLogic
    {
        protected virtual CharacterSubState mainPhase => throw new ArgumentException();
        protected virtual CharacterSubState recoveryPhase => throw new ArgumentException();
        public override void SetAsActiveStateMachineLogic()
        {
            base.SetAsActiveStateMachineLogic();
            stateMachineController.SetActiveMultiStateMachineLogic(this);
        }
        public void GoToMainPhase() => stateMachineController.characterSubState = mainPhase;
        public void GoToRecoveryPhase() => stateMachineController.characterSubState = recoveryPhase;
    }

    public abstract class ChargeableStateMachineLogic : MultiPhaseStateMachineLogic
    {
        protected abstract CharacterSubState chargePhase { get; }
        public void GoToChargePhase() => stateMachineController.characterSubState = chargePhase;
    }
}