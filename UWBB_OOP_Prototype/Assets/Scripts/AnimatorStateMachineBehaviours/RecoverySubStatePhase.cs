using UnityEngine;

namespace UWBB.CharacterController
{
    public class RecoverySubStatePhase : StateMachineBehaviour
    {
        private CharacterController_StatePhase characterControllerStatePhase;

        public void Init(CharacterController_StatePhase controllerStatePhase) 
            => characterControllerStatePhase = controllerStatePhase;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterControllerStatePhase.AdvanceToRecoveryPhase();

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => characterControllerStatePhase.FinishState();
    }
}