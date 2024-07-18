using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterStatePhaseController : MonoBehaviour
    {
        public IThreePhaseStateMachineLogic threePhaseStateMachineLogic;

        public void AdvanceToMainPhase() => threePhaseStateMachineLogic.AdvanceToMainPhase();
        public void AdvanceToRecoveryPhase() => threePhaseStateMachineLogic.AdvanceToRecoveryPhase();
    }
}