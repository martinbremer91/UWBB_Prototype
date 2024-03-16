using UnityEngine;

namespace UWBB.Combat
{
    public class AttackTrigger : MonoBehaviour
    {
        [HideInInspector] public int damage;
        [HideInInspector] public float knockback;

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
