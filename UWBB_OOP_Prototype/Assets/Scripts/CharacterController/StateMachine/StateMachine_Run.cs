using System;
using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class StateMachine_Run : IMultiPhaseStateMachineLogic
    {
        public CharacterController_StateMachine stateMachine { get; set; }
        public CharacterStatePhaseController characterStatePhaseController { get; set; }
        public CharacterController_Animation animationController { get; set; }
        private InputState inputState;
        private CharacterController_Stamina staminaCtrl;
        private StaminaActions staminaActions;

        private float minimumRunTimerForRunningAttack;

        public CharacterSubState startPhase => CharacterSubState.RunStart;
        public CharacterSubState mainPhase => CharacterSubState.RunMain;
        public CharacterSubState recoveryPhase => throw new ArgumentException();
        
        public void Init(GameManager gameManager)
        {
            staminaActions = GameConfigs.instance.staminaActions;
            stateMachine = gameManager.stateMachine;
            staminaCtrl = gameManager.staminaController;
            inputState = gameManager.inputController.inputState;
            characterStatePhaseController = gameManager.characterStatePhaseController;
            animationController = gameManager.animationController;
            minimumRunTimerForRunningAttack = 
                gameManager.player.playerConfigs.minimumRunTimerForRunningAttack;
        }

        public void EnterState()
        {
            stateMachine.currentRunActionTimer = 0;
            // animationController.animator.Play(animationStateID);
        }

        public void ProcessState()
        {
            if (inputState.useItemCommand)
                stateMachine.characterSubState = CharacterSubState.UseItemStart;
            else if (inputState.lightAttackCommand && staminaCtrl.HasStaminaForAction(staminaActions.lightAttack))
                stateMachine.characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.heavyAttackCommand && staminaCtrl.HasStaminaForAction(staminaActions.heavyAttack))
                stateMachine.characterSubState = CharacterSubState.AttackHeavyStart;
            else if (inputState.moveDirection == Vector2.zero)
                stateMachine.characterSubState = CharacterSubState.Idle;
            else if (!inputState.runCommand)
                stateMachine.characterSubState = CharacterSubState.Walk;
            else if (stateMachine.characterSubState is CharacterSubState.RunStart)
                stateMachine.currentRunActionTimer += Time.deltaTime;

            if (stateMachine.currentRunActionTimer >= minimumRunTimerForRunningAttack)
                stateMachine.characterSubState = CharacterSubState.RunMain;
        }

        public void ProcessStateTransition() { }

        public void ExitState() { }
    }
}