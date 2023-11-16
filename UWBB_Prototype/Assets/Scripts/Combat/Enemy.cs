using UnityEngine;
using UWBB.CharacterController;
using UWBB.Interfaces;

namespace UWBB.Combat
{
    public class Enemy : MonoBehaviour, IAttackable, IDamageable, IKnockbackable, ILockOnTarget
    {
        public static Player player;
        
        // temp
        public GameObject go => gameObject;
        
        public Vector3 position => transform.position;
        public IDamageable damageable => this;
        public IKnockbackable knockbackable => this;

        private int healthPoints = 100;

        private void Start() 
            => player.lockOnController.lockOnTargets.Add(this);

        public void TakeDamage(int value)
        {
            healthPoints -= value;
            Debug.Log(healthPoints);
        }

        public void TakeKnockback(Vector3 direction, float value)
        {
            transform.position += direction * value;
            Debug.Log("knockback: " + value);
        }
    }
}