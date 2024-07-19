using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterStatePhaseController : MonoBehaviour
    {
        private StateMachineLogic stateMachineLogic;
        private MultiPhaseStateMachineLogic multiPhaseStateMachineLogic;
        
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
                Debug.LogError("CharacterStatePhaseController: current state is not chargeable", this);
                multiPhaseStateMachineLogic.GoToMainPhase();
            }
        }
    }
}