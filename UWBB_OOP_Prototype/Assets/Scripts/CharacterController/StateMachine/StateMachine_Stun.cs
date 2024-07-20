﻿namespace UWBB.CharacterController
{
    // TODO: implement stun levels
    // flinch           : no interrupt, visual reaction
    // stagger          : short interrupt, player poise depleted, lesser mob poise depleted
    // knockdown        : long interrupt, parry / immobile, player poise break with knockback, boss and greater mob poise depleted
    
    
    // TODO: poise
    // recovery timer (before regen) proportional to total poise
    // elden ring reference: 65 poise -> 5s recovery timer
    // elden ring reference: max player poise is a bit below 14
    // (this is extremely low, comparable to the PvE enemies with the least poise in the game
    // elden ring reference: player recovery timer is always 30s (which might be too high)
    
    
    
    public class StateMachine_Stun : StateMachineLogic
    {
        public override void ProcessStateTransition() 
            => stateMachineController.characterSubState = CharacterSubState.Idle;
    }
}