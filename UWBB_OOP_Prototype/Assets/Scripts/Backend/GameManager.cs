using System;
using System.Collections.Generic;
using UWBB.Configs;

namespace UWBB.CharacterController
{
    public class GameManager
    {
        public static CharacterController_Player characterControllerPlayer;
        [NonSerialized] public CharacterController_Input inputController;
        [NonSerialized] public CharacterController_Stamina staminaController;
        [NonSerialized] public CharacterController_StateMachine stateMachine;
        [NonSerialized] public CharacterController_Camera cameraController;
        
        public static List<int> autoInstantiatedPrefabsGUIDs;

        public GameManager() => autoInstantiatedPrefabsGUIDs = new List<int>();

        public void Init()
        {
            InstantiatePrefabsInConfig();

            characterControllerPlayer.gameManager = this;
            inputController = new CharacterController_Input();
            staminaController = new CharacterController_Stamina();
            stateMachine = new CharacterController_StateMachine();
            cameraController = new CharacterController_Camera();
            
            staminaController.Init(stateMachine);
            stateMachine.Init(inputController, staminaController);
            cameraController.Init(characterControllerPlayer.cameraTransform, characterControllerPlayer.transform);

#if UNITY_EDITOR
            CustomDebug.instance.Init(stateMachine, inputController, staminaController, cameraController);
#endif
        }

        private void InstantiatePrefabsInConfig() => 
            autoInstantiatedPrefabsGUIDs.AddRange(GameConfigs.instance.autoInstantiatingPrefabs.InstantiatePrefabs());
    }
}