using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    
    
    
    
    
    // TODO: create OnEnterState and OnExitState methods alongside ProcessState
    
    
    
    
    public class CharacterController_StateMachine
    {
        private CharacterController_Input inputController;
        private CharacterController_Stamina staminaController;

        private InputState inputState => inputController.inputState;
        private StaminaActions staminaActions;

        public CharacterState characterState { get; private set; }
        private CharacterSubState _characterSubState;
        public CharacterSubState characterSubState
        {
            get => _characterSubState;
            set
            {
                _characterSubState = value;
                characterState = GetCharacterStateFromSubState(_characterSubState);
                ProcessCharacterState();
            }
        }
        
        // TODO: move this somewhere else
        private float minimumRunTimerForRunningAttack;
        private float currentRunActionTimer;

        public void Init(CharacterController_Input inputCtrl, CharacterController_Stamina staminaCtrl)
        {
            staminaActions = GameConfigs.instance.staminaActions;

            inputController = inputCtrl;
            staminaController = staminaCtrl;
        }

        public void Update() => ProcessCharacterState();

        private void ProcessCharacterState()
        {
            switch(characterState)
            {
                case CharacterState.Idle : ProcessIdle(); break;
                case CharacterState.Walk : ProcessWalk(); break;
                case CharacterState.Run : ProcessRun(); break;
                case CharacterState.Dodge : ProcessDodge(); break;
                case CharacterState.AttackLight : ProcessAttackLight(); break;
                case CharacterState.AttackHeavy : ProcessAttackHeavy(); break;
                case CharacterState.UsingItem : ProcessUsingItem(); break;
                default : throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessIdle()
        {
            if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
                characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.moveDirection != Vector2.zero)
                characterSubState = CharacterSubState.Walk;
            else if (inputState.dodgeCommand && staminaController.HasStaminaForAction(staminaActions.dodge))
                characterSubState = CharacterSubState.DodgeStart;
        }

        private void ProcessWalk()
        {
            if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
                characterSubState = CharacterSubState.AttackLightStart;
            else if (inputState.dodgeCommand && staminaController.HasStaminaForAction(staminaActions.dodge))
                characterSubState = CharacterSubState.DodgeStart;
            else if (inputState.runCommand && !staminaController.isWinded && staminaController.HasStaminaForAction(staminaActions.run))
                characterSubState = CharacterSubState.RunStart;
            else if (inputState.moveDirection == Vector2.zero)
                characterSubState = CharacterSubState.Idle;
        }
        
        private void ProcessRun()
        {
            if (inputState.lightAttackCommand && staminaController.HasStaminaForAction(staminaActions.lightAttack))
            {
                currentRunActionTimer = 0;
                characterSubState = CharacterSubState.AttackLightStart;
            }
            else if (inputState.moveDirection == Vector2.zero)
            {
                currentRunActionTimer = 0;
                characterSubState = CharacterSubState.Idle;
            }
            else if (!inputState.runCommand)
            {
                currentRunActionTimer = 0;
                characterSubState = CharacterSubState.Walk;
            }
            else
                currentRunActionTimer += Time.deltaTime;
        }

        private void ProcessDodge()
        {
            staminaController.isWinded = false;

            if (Keyboard.current.iKey.IsPressed())
                characterSubState = CharacterSubState.Idle;
            // eventually: buffer dodge attack
        }

        private void ProcessAttackLight()
        {
            staminaController.isWinded = false;
            
            if (Keyboard.current.iKey.IsPressed())
                characterSubState = CharacterSubState.Idle;
            // set isWinded to false
            // eventually: buffer combo attack
        }

        private void ProcessAttackHeavy()
        {
            staminaController.isWinded = false;
            
            if (Keyboard.current.iKey.IsPressed())
                characterSubState = CharacterSubState.Idle;
            // set isWinded to false
            // eventually: buffer combo attack
        }

        private void ProcessUsingItem()
        {
            staminaController.isWinded = false;
            
            if (Keyboard.current.iKey.IsPressed())
                characterSubState = CharacterSubState.Idle;
            // set isWinded to false
            // might be unnecessary? state can only elapse or be interrupted by external factors
            // maybe set move speed to "slowed"
        }
        
        private CharacterState GetCharacterStateFromSubState(CharacterSubState subState)
        {
            switch (subState)
            {
                case CharacterSubState.Idle : return CharacterState.Idle;
                case CharacterSubState.Walk : return CharacterState.Walk;
                case CharacterSubState.DodgeStart or CharacterSubState.DodgeMain or CharacterSubState.DodgeRecovery : 
                    return CharacterState.Dodge;
                case CharacterSubState.RunStart or CharacterSubState.RunMain or CharacterSubState.RunRecovery :
                    return CharacterState.Run;
                case CharacterSubState.AttackLightStart or CharacterSubState.AttackLightMain or CharacterSubState.AttackLightRecovery:
                    return CharacterState.AttackLight;
                case CharacterSubState.AttackHeavyStart or CharacterSubState.AttackHeavyCharge 
                    or CharacterSubState.AttackHeavyMain or CharacterSubState.AttackHeavyRecovery :
                    return CharacterState.AttackHeavy;
                case CharacterSubState.UsingItemStart or CharacterSubState.UsingItemMain or CharacterSubState.UsingItemRecovery:
                    return CharacterState.UsingItem;
                case CharacterSubState.StunnedMain or CharacterSubState.StunnedRecovery:
                    return CharacterState.Stunned;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}