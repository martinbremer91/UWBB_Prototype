using System;
using System.Collections.Generic;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    
    
    
    // TODO: make initialization character-agnostic and modular
    // related: Create character-agnostic spawn point system
    
    
    
    
    public class GameManager
    {
        [NonSerialized] public CharacterController_Player player;
        [NonSerialized] public CharacterStatePhaseController characterStatePhaseController;
        
        [NonSerialized] public CharacterController_Input inputController;
        [NonSerialized] public CharacterController_Stamina staminaController;
        [NonSerialized] public CharacterController_StateMachine stateMachine;
        [NonSerialized] public CharacterController_Camera cameraController;
        [NonSerialized] public CharacterController_Animation animationController;
        
        public static List<int> autoInstantiatedPrefabsGUIDs;
        public GameManager() => autoInstantiatedPrefabsGUIDs = new List<int>();

        public void Init()
        {
            InstantiatePrefabsInConfig();
            
            characterStatePhaseController = player.characterStatePhaseController;
            inputController = new CharacterController_Input();
            staminaController = new CharacterController_Stamina();
            stateMachine = new CharacterController_StateMachine();
            cameraController = new CharacterController_Camera();
            animationController = new CharacterController_Animation(); 
            
            player.Init(this);
            staminaController.Init(stateMachine);
            stateMachine.Init(this);
            cameraController.Init(player.cameraTransform, player.transform);
            animationController.Init(this);
#if UNITY_EDITOR
            CustomDebug.instance.Init(this);
#endif
        }

        private void InstantiatePrefabsInConfig() =>
            autoInstantiatedPrefabsGUIDs.AddRange(
                GameConfigs.instance.autoInstantiatingPrefabs.InstantiatePrefabs(this));
    }
}