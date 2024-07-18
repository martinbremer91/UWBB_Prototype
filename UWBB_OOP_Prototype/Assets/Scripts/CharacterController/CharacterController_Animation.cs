using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Animation
    {
        public Animator animator;
        
        public void Init(CharacterController_Player player)
        {
            animator = player.GetComponent<Animator>();
        }
    }
}