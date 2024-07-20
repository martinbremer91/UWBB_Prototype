using UnityEngine;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class CharacterController_Stamina : ICharacterController
    {
        private CharacterController_StateMachine stateMachine;
        private StaminaActions staminaActions;

        private StaminaState _staminaState;
        private StaminaState staminaState
        {
            get => _staminaState;
            set
            {
                _staminaState = value;
                isWinded = _staminaState is StaminaState.Empty 
                           || (_staminaState is not StaminaState.Full && isWinded);
            }
        }
        
        private float staminaTotal ;
        private float regenRate;
        private float staminaRegenBlockDuration;
        
        private float staminaRegenBlockTimer;
        private float stamina;
        
        public bool isWinded;
        private bool regenThisFrame;

        public void Init(ICharacter character)
        {
            staminaActions = GameConfigs.instance.staminaActions;
            stateMachine = character.GetModuleController<CharacterController_StateMachine>(ControllerType.StateMachine);

            CharacterConfigs configs = character.characterConfigs;

            staminaTotal = configs.staminaTotal;
            regenRate = configs.regenRate;
            staminaRegenBlockDuration = configs.staminaRegenBlockDuration;
            
            stamina = staminaTotal;
        }
        
        public void Update()
        {
            staminaRegenBlockTimer = Mathf.Max(0, staminaRegenBlockTimer - Time.deltaTime);
            regenThisFrame = staminaState is not StaminaState.Full && staminaRegenBlockTimer == 0;
            
            ProcessStaminaConsumption();
            
            if (regenThisFrame)
                RegenerateStamina();
        }

        private void ProcessStaminaConsumption()
        {
            if (stateMachine.characterState is CharacterState.Run)
                ConsumeStamina(staminaActions.run);
        }

        public bool HasStaminaForAction(StaminaAction action) => stamina >= action.minToPerform;

        private void ConsumeStamina(StaminaAction action)
        {
            staminaState = StaminaState.Consume;
            
            float cost = action.isCostOverTime ? action.cost * Time.deltaTime : action.cost;
            stamina -= cost;

            if (Mathf.Approximately(stamina, 0))
                staminaState = StaminaState.Empty;

            staminaRegenBlockTimer = staminaRegenBlockDuration;
        }

        private void RegenerateStamina()
        {
            staminaState = StaminaState.Regenerate;
            
            stamina = Mathf.Min(staminaTotal, stamina + regenRate * Time.deltaTime);

            if (Mathf.Approximately(stamina, staminaTotal))
                staminaState = StaminaState.Full;
        }
    }

    public enum StaminaState
    {
        Full,
        Consume,
        Regenerate,
        Empty,
    }
}