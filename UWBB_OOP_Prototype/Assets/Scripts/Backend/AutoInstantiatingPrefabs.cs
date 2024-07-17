using System.Collections.Generic;
using UnityEngine;
using UWBB.CharacterController;

namespace UWBB.Configs
{
    [CreateAssetMenu(fileName = "AutoInstantiatingPrefabs", menuName = "UWBB/AutoInstantiatingPrefabs")]
    public class AutoInstantiatingPrefabs : ScriptableObject
    {
        public List<GameObject> prefabs;

        public List<int> InstantiatePrefabs()
        {
            List<int> prefabGuids = new();
            
            foreach (GameObject prefab in prefabs)
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
    }
}