using UnityEngine;

namespace UWBB.CharacterController
{
    public class RecoverySubStatePhase : StateMachineBehaviour
    {
        private CharacterStatePhaseController characterStatePhaseController;

        public void Init(CharacterStatePhaseController statePhaseController) 
            => characterStatePhaseController = statePhaseController;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterStatePhaseController.AdvanceToRecoveryPhase();

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterStatePhaseController.FinishState();
    }
}