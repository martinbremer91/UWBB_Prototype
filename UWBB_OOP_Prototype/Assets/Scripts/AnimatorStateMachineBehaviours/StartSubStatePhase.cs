using UnityEngine;

namespace UWBB.CharacterController
{
    public class StartSubStatePhase : StateMachineBehaviour
    {
        private CharacterStatePhaseController characterStatePhaseController;

        public void Init(CharacterStatePhaseController statePhaseController) 
            => characterStatePhaseController = statePhaseController;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterStatePhaseController.BeginState();
    }
}