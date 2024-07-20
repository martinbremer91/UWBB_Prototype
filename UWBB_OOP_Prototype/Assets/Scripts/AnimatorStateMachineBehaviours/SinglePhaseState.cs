using UnityEngine;

namespace UWBB.CharacterController
{
    public class SinglePhaseState : StateMachineBehaviour
    {
        private CharacterController_StateMachine stateMachineController;

        public void Init(CharacterController_StateMachine stateMachine) 
            => stateMachineController = stateMachine;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => stateMachineController.FinishState();
    }
}