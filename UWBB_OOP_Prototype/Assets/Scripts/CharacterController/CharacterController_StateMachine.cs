using System;
using System.Collections.Generic;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_StateMachine : ICharacterController
    {
        private readonly Dictionary<CharacterState, StateMachineLogic> stateMachineLogicDict = new();
        
        public CharacterState characterState { get; private set; }
        private CharacterSubState _characterSubState;
        public CharacterSubState characterSubState
        {
            get => _characterSubState;
            set
            {
                _characterSubState = value;
                CharacterState newState = GetCharacterStateFromSubState(value);

                if (CheckStateTransitionLegality(newState))
                {
                    stateMachineLogicDict[characterState].ExitState();
                    characterState = newState;
                    
                    StateMachineLogic newLogic = stateMachineLogicDict[characterState];
                    newLogic.SetAsActiveStateMachineLogic();
                    newLogic.EnterState(_characterSubState);
                }
            }
        }
        
        private StateMachineLogic stateMachineLogic;
        private MultiPhaseStateMachineLogic multiPhaseStateMachineLogic;
        
        public float currentRunActionTimer;

        public void Init(ICharacter character)
        {
            stateMachineLogicDict.Add(CharacterState.Idle, new StateMachine_Idle());
            stateMachineLogicDict.Add(CharacterState.Walk, new StateMachine_Walk());
            stateMachineLogicDict.Add(CharacterState.Run, new StateMachine_Run());
            stateMachineLogicDict.Add(CharacterState.Dodge, new StateMachine_Dodge());
            stateMachineLogicDict.Add(CharacterState.AttackLight, new StateMachine_LightAttack());
            stateMachineLogicDict.Add(CharacterState.AttackHeavy, new StateMachine_HeavyAttack());
            stateMachineLogicDict.Add(CharacterState.UsingItem, new StateMachine_UsingItem());
            stateMachineLogicDict.Add(CharacterState.Stun, new StateMachine_Stun());

            foreach (var logic in stateMachineLogicDict.Values)
                logic.Init(character);
        }

        public void Update() => ProcessCharacterState();
        private void ProcessCharacterState() => stateMachineLogicDict[characterState].ProcessState();
        
        public void SetActiveStateMachineLogic(StateMachineLogic logic)
            => stateMachineLogic = logic;
        public void SetActiveMultiStateMachineLogic(MultiPhaseStateMachineLogic logic) 
            => multiPhaseStateMachineLogic = logic;

        public void AdvanceToMainPhase() => multiPhaseStateMachineLogic.GoToMainPhase();
        public void AdvanceToRecoveryPhase() => multiPhaseStateMachineLogic.GoToRecoveryPhase();
        public void FinishState() => stateMachineLogic.ProcessStateTransition();
        
        public void BeginChargePhase()
        {
            if (multiPhaseStateMachineLogic is ChargeableStateMachineLogic chargeable)
                chargeable.GoToChargePhase();
            else
            {
                Debug.LogError("CharacterStatePhaseController: current state is not chargeable");
                multiPhaseStateMachineLogic.GoToMainPhase();
            }
        }
        
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
                case CharacterSubState.StunFlinch or CharacterSubState.StunStagger or CharacterSubState.StunKnockdown:
                    return CharacterState.Stun;
            }

            throw new ArgumentOutOfRangeException();
        }

        private bool CheckStateTransitionLegality(CharacterState newState)
        {
            if (newState is CharacterState.Stun)
                return true;
            return characterState != newState;
        }
    }
}