using System;
using UnityEngine;

namespace UWBB.GameFramework
{
    public class Main : MonoBehaviour
    {
        [NonSerialized] public Main instance;
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
