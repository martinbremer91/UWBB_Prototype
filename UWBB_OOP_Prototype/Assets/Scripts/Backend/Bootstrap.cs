using System;
using System.Collections.Generic;
using UnityEngine;

namespace UWBB.Configs
{
    public class Bootstrap : MonoBehaviour
    {
        private static Bootstrap instance;
        [SerializeField] private GameConfigs gameConfigs;

        [NonSerialized] public readonly List<int> autoInstantiatedPrefabsGUIDs = new List<int>();
        
        private void OnEnable()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            gameConfigs.Init();
            InstantiatePrefabsInConfig();
        }
        
        private void InstantiatePrefabsInConfig() =>
            autoInstantiatedPrefabsGUIDs.AddRange(
                GameConfigs.instance.autoInstantiatingPrefabs.InstantiatePrefabs(this));
    }
}