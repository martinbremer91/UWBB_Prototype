using System;
using System.Collections.Generic;

namespace UWBB.CharacterController
{
    public class CharacterController_StateMachine
    {
        private Dictionary<CharacterState, StateMachineLogic> stateMachineLogicDict = new();
        
        public CharacterState characterState { get; private set; }
        private CharacterSubState _characterSubState;
        public CharacterSubState characterSubState
        {
            get => _characterSubState;
            set
            {
                _characterSubState = value;
                CharacterState newState = GetCharacterStateFromSubState(value);

                if (characterState != newState)
                {
                    stateMachineLogicDict[characterState].ExitState();
                    characterState = newState;
                    
                    StateMachineLogic newLogic = stateMachineLogicDict[characterState];
                    newLogic.SetAsActiveStateMachineLogic();
                    newLogic.EnterState();
                }
            }
        }
        
        public float currentRunActionTimer;

        public void Init(GameManager gameManager)
        {
            stateMachineLogicDict.Add(CharacterState.Idle, new StateMachine_Idle());
            stateMachineLogicDict.Add(CharacterState.Walk, new StateMachine_Walk());
            stateMachineLogicDict.Add(CharacterState.Run, new StateMachine_Run());
            stateMachineLogicDict.Add(CharacterState.Dodge, new StateMachine_Dodge());
            stateMachineLogicDict.Add(CharacterState.AttackLight, new StateMachine_LightAttack());
            stateMachineLogicDict.Add(CharacterState.AttackHeavy, new StateMachine_HeavyAttack());
            stateMachineLogicDict.Add(CharacterState.UsingItem, new StateMachine_UsingItem());
            stateMachineLogicDict.Add(CharacterState.Stunned, new StateMachine_Stun());

            foreach (var stateMachineLogic in stateMachineLogicDict.Values)
                stateMachineLogic.Init(gameManager);
        }

        public void Update() => ProcessCharacterState();
        private void ProcessCharacterState() => stateMachineLogicDict[characterState].ProcessState();
        
        private CharacterState GetCharacterStateFromSubState(CharacterSubState subState)
        {
            switch (subState)
            {
                case CharacterSubState.Idle : return CharacterState.Idle;
                case CharacterSubState.Walk : return CharacterState.Walk;
                case CharacterSubState.DodgeStart or CharacterSubState.DodgeMain or CharacterSubState.DodgeRecovery : 
                    return CharacterState.Dodge;
                case CharacterSubState.RunStart or CharacterSubState.RunMain :
                    return CharacterState.Run;
                case CharacterSubState.AttackLightStart or CharacterSubState.AttackLightMain or CharacterSubState.AttackLightRecovery:
                    return CharacterState.AttackLight;
                case CharacterSubState.AttackHeavyStart or CharacterSubState.AttackHeavyCharge 
                    or CharacterSubState.AttackHeavyMain or CharacterSubState.AttackHeavyRecovery :
                    return CharacterState.AttackHeavy;
                case CharacterSubState.UseItemStart or CharacterSubState.UseItemMain or CharacterSubState.UseItemRecovery:
                    return CharacterState.UsingItem;
                case CharacterSubState.StunMain or CharacterSubState.StunRecovery:
                    return CharacterState.Stunned;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}