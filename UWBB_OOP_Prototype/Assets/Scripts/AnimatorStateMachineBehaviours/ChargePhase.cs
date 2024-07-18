using UnityEngine;
using UWBB.CharacterController;

public class ChargePhase : StateMachineBehaviour
{
    private CharacterStatePhaseController characterStatePhaseController;
    private InputState inputState;
    private static readonly int ChargeRelease = Animator.StringToHash("ChargeRelease");

    public void Init(GameManager gameManager)
    {
        characterStatePhaseController = gameManager.characterStatePhaseController;
        inputState = gameManager.inputController.inputState;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        => characterStatePhaseController.BeginChargePhase();

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!inputState.heavyAttackHeld)
            animator.SetTrigger(ChargeRelease);
    }
}