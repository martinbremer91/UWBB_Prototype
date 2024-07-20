using System.Collections.Generic;
using UnityEngine;

namespace UWBB.Configs
{
    [CreateAssetMenu(fileName = "AutoInstantiatingPrefabs", menuName = "UWBB/AutoInstantiatingPrefabs")]
    public class AutoInstantiatingPrefabs : ScriptableObject
    {
        public List<GameObject> autoPrefabs;
#if UNITY_EDITOR
        public List<GameObject> editorOnlyAutorPrefab;
#endif

        public List<int> InstantiatePrefabs(Bootstrap bootstrap)
        {
            List<int> prefabGuids = InstantiatePrefabList(bootstrap, autoPrefabs);
#if UNITY_EDITOR
            prefabGuids.AddRange(InstantiatePrefabList(bootstrap, editorOnlyAutorPrefab));
#endif
            return prefabGuids;
        }

        private List<int> InstantiatePrefabList(Bootstrap bootstrap, List<GameObject> list)
        {
            List<int> prefabGuids = new();
            
            foreach (GameObject prefab in list)
            {
                int guid = prefab.gameObject.GetInstanceID();
                
                if (bootstrap.autoInstantiatedPrefabsGUIDs.Contains(guid))
                    continue;
                
                prefabGuids.Add(guid);
                GameObject instance = Instantiate(prefab);
                DontDestroyOnLoad(instance);
            }

            return prefabGuids;
        }
    }
}