using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Animation
    {
        public Animator animator;
        
        public void Init(GameManager gameManager)
        {
            animator = gameManager.player.GetComponent<Animator>();

            foreach (var start in animator.GetBehaviours<StartSubStatePhase>())
                start.Init(gameManager.characterStatePhaseController);
            foreach (var main in animator.GetBehaviours<MainSubStatePhase>())
                main.Init(gameManager.characterStatePhaseController);
            foreach (var recovery in animator.GetBehaviours<RecoverySubStatePhase>())
                recovery.Init(gameManager.characterStatePhaseController);
            
            foreach (var preCharge in animator.GetBehaviours<PreChargePhase>())
                preCharge.Init(gameManager);
            foreach (var charge in animator.GetBehaviours<ChargePhase>())
                charge.Init(gameManager);
        }
    }
}