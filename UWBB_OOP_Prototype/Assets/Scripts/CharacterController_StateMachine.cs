using System;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_StateMachine
    {
        // TODO:
        // dodge input needs a duration to check the dodgeRunInputTimer against
        // running attacks need a duration to check the currentRunActionTimer against
        
        private float dodgeRunInputTimer;
        private float currentRunActionTimer;
        
        public void Update(CharacterControllerState state)
        {
            ProcessRunDodgeInput(state.inputState.runCommand);
            
            switch(state.characterState)
            {
                case CharacterState.Idle : ProcessIdle(state); break;
                case CharacterState.Walk : ProcessWalk(state); break;
                case CharacterState.Run : ProcessRun(state); break;
                case CharacterState.Dodge : ProcessDodge(state); break;
                case CharacterState.Attack : ProcessAttack(state); break;
                case CharacterState.UsingItem : ProcessUsingItem(state); break;
                default : throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessRunDodgeInput(bool runCommand)
        {
            if (runCommand)
                dodgeRunInputTimer += Time.deltaTime;
            else
                dodgeRunInputTimer = 0;
        }

        private void ProcessIdle(CharacterControllerState state)
        {
            InputState input = state.inputState;

            // attack
            // run command
            // hold direction
        }

        private void ProcessWalk(CharacterControllerState state)
        {
            // release direction
            // run command
            // attack
        }
        
        private void ProcessRun(CharacterControllerState state)
        {
            // release direction
            // attack
            // release run input
        }

        private void ProcessDodge(CharacterControllerState state)
        {
            // eventually: buffer dodge attack
        }

        private void ProcessAttack(CharacterControllerState state)
        {
            // eventually: buffer combo attack
        }

        private void ProcessUsingItem(CharacterControllerState state)
        {
            // might be unnecessary? state can only elapse or be interrupted by external factors
            // maybe set move speed to "slowed"
        }
    }
}