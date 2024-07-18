using UnityEngine;
using UWBB.CharacterController;

public class PreChargePhase : StateMachineBehaviour
{
    private InputState inputState;
    private bool isCharging;
    private static readonly int Charge = Animator.StringToHash("Charge");

    public void Init(GameManager gameManager) => inputState = gameManager.inputController.inputState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        => isCharging = inputState.heavyAttackHeld;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isCharging)
        {
            isCharging = inputState.heavyAttackHeld;
            animator.SetBool(Charge, true);

            if (!isCharging)
                animator.SetBool(Charge, false);
        }
    }
}