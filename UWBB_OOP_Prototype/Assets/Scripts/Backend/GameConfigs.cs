using UnityEngine;
using UWBB.CharacterController;

namespace  UWBB.Configs
{
    public class GameConfigs : ScriptableObject
    {
        public static GameConfigs instance;

        public AutoInstantiatingPrefabs autoInstantiatingPrefabs;
        public StaminaActions staminaActions;

        public void Init() => instance = this;
    }
}
