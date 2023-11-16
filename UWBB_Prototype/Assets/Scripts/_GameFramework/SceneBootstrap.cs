using UnityEngine;
using UWBB.CharacterController;
using UWBB.Combat;

namespace UWBB.GameFramework
{
    public class SceneBootstrap : MonoBehaviour
    {
        public Player player;
        
        private void Awake()
        {
            player.Init();
            Enemy.player = player;
        }

        private void Start() => Destroy(gameObject);
    }
}