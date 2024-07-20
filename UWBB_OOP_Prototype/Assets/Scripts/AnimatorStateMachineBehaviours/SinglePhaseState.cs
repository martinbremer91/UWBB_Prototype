using UnityEngine;

namespace UWBB.CharacterController
{
    public class SinglePhaseState : StateMachineBehaviour
    {
        private CharacterController_StateMachine stateMachineController;

        public void Init(CharacterController_StateMachine stateMachine) 
            => stateMachineController = stateMachine;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (HasReachedExitTime(stateInfo))
                stateMachineController.FinishState();
        }

        private bool HasReachedExitTime(AnimatorStateInfo info) => info.normalizedTime >= 1;
    }
}