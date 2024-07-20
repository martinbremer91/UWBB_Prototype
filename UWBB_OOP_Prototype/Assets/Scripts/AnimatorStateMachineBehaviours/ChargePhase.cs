using UnityEngine;
using UWBB.CharacterController;

public class ChargePhase : StateMachineBehaviour
{
    private CharacterController_StatePhase statePhase;
    private InputState inputState;

    public void Init(ICharacter character)
    {
        statePhase = character.GetModuleController<CharacterController_StatePhase>(ControllerType.StatePhase);
        inputState = character.GetModuleController<CharacterController_Input>(ControllerType.Input).inputState;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        => statePhase.BeginChargePhase();

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!inputState.heavyAttackHeld)
            animator.SetTrigger(AnimationConstants.chargeRelease);
    }
}