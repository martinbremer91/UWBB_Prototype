using UnityEngine;

namespace ECS
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this) 
                Destroy(gameObject);
        }
    }
}