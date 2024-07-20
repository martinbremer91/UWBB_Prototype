using UnityEngine;

namespace UWBB.CharacterController
{
    public class MainSubStatePhase : StateMachineBehaviour
    {
        private CharacterController_StateMachine stateMachineController;

        public void Init(CharacterController_StateMachine stateMachine) 
            => stateMachineController = stateMachine;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => stateMachineController.AdvanceToMainPhase();
    }
    
    public class ChargeStatePhase : StateMachineBehaviour
    {
        private CharacterController_StateMachine stateMachineController;

        public void Init(CharacterController_StateMachine stateMachine) 
            => stateMachineController = stateMachine;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => stateMachineController.AdvanceToMainPhase();
    }
}