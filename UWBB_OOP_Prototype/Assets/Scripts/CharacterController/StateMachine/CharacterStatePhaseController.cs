using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterStatePhaseController : MonoBehaviour
    {
        private IStateMachineLogic stateMachineLogic;
        private IMultiPhaseStateMachineLogic multiPhaseStateMachineLogic;
        
        public void SetActiveStateMachineLogic(IStateMachineLogic logic)
        {
            stateMachineLogic = logic;
            multiPhaseStateMachineLogic = logic as IMultiPhaseStateMachineLogic;
        }

        public void BeginState() => multiPhaseStateMachineLogic.GoToStartPhase();
        public void AdvanceToMainPhase() => multiPhaseStateMachineLogic.GoToMainPhase();
        public void AdvanceToRecoveryPhase() => multiPhaseStateMachineLogic.GoToRecoveryPhase();
        public void FinishState() => stateMachineLogic.ProcessStateTransition();
        
        public void BeginChargePhase()
        {
            if (multiPhaseStateMachineLogic is IChargeableStateMachineLogic chargeable)
                chargeable.GoToChargePhase();
            else
            {
                Debug.LogError("CharacterStatePhaseController: current state is not chargeable", this);
                multiPhaseStateMachineLogic.GoToMainPhase();
            }
        }
    }
}