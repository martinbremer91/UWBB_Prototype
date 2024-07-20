using UnityEngine;

namespace UWBB.CharacterController
{
    // TODO: consolidate this with StateMachine controller
    
    public class CharacterController_StatePhase : ICharacterController
    {
        private StateMachineLogic stateMachineLogic;
        private MultiPhaseStateMachineLogic multiPhaseStateMachineLogic;
        
        // TEMP: class will be consolidated with StateMachine controller
        public void Init(ICharacter character)
        {
        }
        
        public void SetActiveStateMachineLogic(StateMachineLogic logic)
        {
            stateMachineLogic = logic;
            multiPhaseStateMachineLogic = logic as MultiPhaseStateMachineLogic;
        }

        public void BeginState() => multiPhaseStateMachineLogic.GoToStartPhase();
        public void AdvanceToMainPhase() => multiPhaseStateMachineLogic.GoToMainPhase();
        public void AdvanceToRecoveryPhase() => multiPhaseStateMachineLogic.GoToRecoveryPhase();
        public void FinishState() => stateMachineLogic.ProcessStateTransition();
        
        public void BeginChargePhase()
        {
            if (multiPhaseStateMachineLogic is ChargeableStateMachineLogic chargeable)
                chargeable.GoToChargePhase();
            else
            {
                Debug.LogError("CharacterStatePhaseController: current state is not chargeable");
                multiPhaseStateMachineLogic.GoToMainPhase();
            }
        }
    }
}