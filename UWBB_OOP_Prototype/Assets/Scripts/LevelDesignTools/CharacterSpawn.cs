using UnityEngine;
using Utilities;

namespace UWBB.CharacterController.LevelDesignTools
{
    public class CharacterSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject characterPrefab;

        private void Awake()
        {
            Transform characterTransform = Instantiate(characterPrefab).transform;
            characterTransform.position = transform.position;
            characterTransform.rotation = transform.rotation;
        }

        #region AVATAR INSTANCING
#if UNITY_EDITOR
        [SerializeField, ReadOnly] private GameObject characterAvatar;
        private bool displayAvatar = true;

        private void Start() => characterAvatar.SetActive(false);

        private void OnDrawGizmos()
        {
            if (!displayAvatar)
                return;
            if (characterAvatar == null)
                CreateAvatarInstance();
            
            if (displayAvatar == Application.isPlaying)
            {
                if (characterAvatar != null)
                    characterAvatar.SetActive(!Application.isPlaying);
                
                displayAvatar = !Application.isPlaying;
            }

            void CreateAvatarInstance()
            {
                if (characterPrefab == null)
                    return;
                
                characterAvatar = new GameObject($"{characterPrefab.name}_Avatar");
                characterAvatar.tag = "EditorOnly";
                characterAvatar.transform.parent = transform;
                characterAvatar.transform.position = transform.position;
                characterAvatar.transform.rotation = transform.rotation;
                characterAvatar.transform.localScale = transform.localScale;
                
                if (characterPrefab.TryGetComponent(out MeshRenderer meshRenderer) 
                    && characterPrefab.TryGetComponent(out MeshFilter meshFilter))
                {
                    characterAvatar.AddComponent<MeshFilter>().sharedMesh = meshFilter.sharedMesh;
                    characterAvatar.AddComponent<MeshRenderer>().sharedMaterials = meshRenderer.sharedMaterials;
                }
                
                RecreateCharacterHierarchy(characterPrefab, characterAvatar.transform);
            }

            void RecreateCharacterHierarchy(GameObject currentPrefabObject, Transform parent)
            {
                foreach (Transform child in currentPrefabObject.transform)
                {
                    GameObject clone = new GameObject($"{child.name}_Avatar");
                    clone.tag = "EditorOnly";
                    clone.transform.parent = parent;
                    clone.transform.position = child.position;
                    clone.transform.rotation = child.rotation;
                    clone.transform.localScale = child.localScale;
                    
                    if (child.TryGetComponent(out MeshRenderer meshRenderer) 
                        && child.TryGetComponent(out MeshFilter meshFilter))
                    {
                        if (child.gameObject.activeSelf)
                        {
                            clone.AddComponent<MeshFilter>().sharedMesh = meshFilter.sharedMesh;
                            clone.AddComponent<MeshRenderer>().sharedMaterials = meshRenderer.sharedMaterials;
                        }
                    }
                    
                    RecreateCharacterHierarchy(child.gameObject, clone.transform);
                }
            }
        }
#endif
        #endregion
    }
}