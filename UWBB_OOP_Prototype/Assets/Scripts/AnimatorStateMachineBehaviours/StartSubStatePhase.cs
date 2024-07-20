using UnityEngine;

namespace UWBB.CharacterController
{
    public class StartSubStatePhase : StateMachineBehaviour
    {
        private CharacterController_StatePhase characterControllerStatePhase;

        public void Init(CharacterController_StatePhase controllerStatePhase) 
            => characterControllerStatePhase = controllerStatePhase;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterControllerStatePhase.BeginState();
    }
}