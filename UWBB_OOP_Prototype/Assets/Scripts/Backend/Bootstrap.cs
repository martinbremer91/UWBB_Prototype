using UnityEngine;
using UWBB.CharacterController;

namespace UWBB.Configs
{
    public class Bootstrap : MonoBehaviour
    {
        public static Bootstrap instance;
        [SerializeField] private GameConfigs gameConfigs;

#if UNITY_EDITOR
        [SerializeField] private GameObject customDebugPrefab;
#endif
        
        private void OnEnable()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
                return;
            
#if UNITY_EDITOR
            Instantiate(customDebugPrefab);
#endif      
            gameConfigs.Init();
            new GameManager().Init();
        }
    }
}