using UnityEngine;
using UWBB.CharacterController;

public class ChargePhase : StateMachineBehaviour
{
    private CharacterController_StateMachine stateMachineController;
    private InputState inputState;

    public void Init(ICharacter character)
    {
        stateMachineController =
            character.GetModuleController<CharacterController_StateMachine>(ControllerType.StateMachine);
        inputState = character.GetModuleController<CharacterController_Input>(ControllerType.Input).inputState;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        => stateMachineController.BeginChargePhase();

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!inputState.heavyAttackHeld)
            animator.SetTrigger(AnimationConstants.chargeRelease);
    }
}