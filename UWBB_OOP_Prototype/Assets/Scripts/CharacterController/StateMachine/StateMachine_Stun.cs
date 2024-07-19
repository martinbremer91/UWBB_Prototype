namespace UWBB.CharacterController
{
    
    
    // TODO: implement levels of stun :
    // - flinch (hit / visual reaction)
    // - stagger (poise depleted / small interrupt)
    // - knockdown (parried / immobile)
    
    // TODO: poise
    // recovery timer (before regen) proportional to total poise
    // elden ring reference: 65 poise -> 5s recovery timer
    // elden ring reference: max player poise is a bit below 14
    // (this is extremely low, comparable to the PvE enemies with the least poise in the game
    // elden ring reference: player recovery timer is always 30s (which might be too high)
    
    
    
    public class StateMachine_Stun : MultiPhaseStateMachineLogic
    {
        protected override CharacterSubState startPhase => CharacterSubState.RunStart;
        protected override CharacterSubState mainPhase => CharacterSubState.RunMain;

        public override void EnterState()
        {
            staminaController.isWinded = false;
            base.EnterState();
        }

        public override void ProcessStateTransition() => stateMachine.characterSubState = CharacterSubState.Idle;
    }
}