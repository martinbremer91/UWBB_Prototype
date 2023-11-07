using UnityEngine;

namespace UWBB.GameFramework
{
    public class Main : MonoBehaviour
    {
        public static Main instance;
        public MainConfigs configs;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}
