using UnityEngine;
using UWBB.CharacterController;

namespace UWBB.GameFramework
{
    public class SceneBootstrap : MonoBehaviour
    {
        public Player player;

        private void Awake()
        {
            player.Init();
            Destroy(gameObject);
        }
    }
}