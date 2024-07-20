using UnityEngine;

namespace UWBB.CharacterController
{
    public class MainSubStatePhase : StateMachineBehaviour
    {
        private CharacterController_StatePhase characterControllerStatePhase;

        public void Init(CharacterController_StatePhase controllerStatePhase) 
            => characterControllerStatePhase = controllerStatePhase;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterControllerStatePhase.AdvanceToMainPhase();
    }
    
    public class ChargeStatePhase : StateMachineBehaviour
    {
        private CharacterController_StatePhase characterControllerStatePhase;

        public void Init(CharacterController_StatePhase controllerStatePhase) 
            => characterControllerStatePhase = controllerStatePhase;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterControllerStatePhase.AdvanceToMainPhase();
    }
}