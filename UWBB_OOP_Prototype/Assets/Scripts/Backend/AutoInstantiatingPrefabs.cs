using System.Collections.Generic;
using UnityEngine;
using UWBB.CharacterController;

namespace UWBB.Configs
{
    [CreateAssetMenu(fileName = "AutoInstantiatingPrefabs", menuName = "UWBB/AutoInstantiatingPrefabs")]
    public class AutoInstantiatingPrefabs : ScriptableObject
    {
        public GameObject playerPrefab;
        public List<GameObject> managerPrefabs;

        public List<int> InstantiatePrefabs(GameManager gameManager)
        {
            BootstrapPlayer(gameManager);
            
            List<int> prefabGuids = new();
            
            foreach (GameObject prefab in managerPrefabs)
            {
                int guid = prefab.gameObject.GetInstanceID();
                
                if (GameManager.autoInstantiatedPrefabsGUIDs.Contains(guid))
                    continue;
                
                prefabGuids.Add(guid);
                GameObject instance = Instantiate(prefab);
                DontDestroyOnLoad(instance);
            }

            return prefabGuids;
        }

        private void BootstrapPlayer(GameManager gameManager)
            => gameManager.player = Instantiate(playerPrefab).GetComponent<CharacterController_Player>();
    }
}