using UnityEngine;
using UWBB.CharacterController;

public class PreChargePhase : StateMachineBehaviour
{
    private InputState inputState;
    private bool isCharging;

    public void Init(ICharacter character) => inputState =
        character.GetModuleController<CharacterController_Input>(ControllerType.Input).inputState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        => isCharging = inputState.heavyAttackHeld;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isCharging)
        {
            isCharging = inputState.heavyAttackHeld;
            animator.SetBool(AnimationConstants.charge, true);

            if (!isCharging)
                animator.SetBool(AnimationConstants.charge, false);
        }
    }
}