using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCameraSingleton : MonoBehaviour
    {
        public static Camera instance;
        private Camera camRef;

        private void OnEnable()
        {
            if (!instance)
            {
                camRef = GetComponent<Camera>();
                instance = camRef;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (instance == camRef)
                instance = null;
        }
    }
}