using System;
using UnityEngine.InputSystem;

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
    
    
    
    
    public class StateMachine_Stun : IMultiPhaseStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        public CharacterController_Animation animationController { get; set; }
        private CharacterController_Stamina staminaCtrl;

        public CharacterSubState startPhase => CharacterSubState.RunStart;
        public CharacterSubState mainPhase => CharacterSubState.RunMain;
        public CharacterSubState recoveryPhase => throw new ArgumentException();
        
        public void Init(GameManager gameManager)
        {
            stateMachine = gameManager.stateMachine;
            staminaCtrl = gameManager.staminaController;
            characterStatePhaseController = gameManager.characterStatePhaseController;
            animationController = gameManager.animationController;
        }

        public void EnterState()
        {
            staminaCtrl.isWinded = false;
        }

        public void ProcessState()
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
                stateMachine.characterSubState = CharacterSubState.Idle;
        }

        public void ProcessStateTransition()
        {
            stateMachine.characterSubState = CharacterSubState.Idle;
        }

        public void ExitState()
        {
        }
    }
}