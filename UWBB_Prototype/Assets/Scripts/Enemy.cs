using UnityEngine;

namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour, IAttackable, IDamageable, IKnockbackable
    {
        public IDamageable damageable => this;
        public IKnockbackable knockbackable => this;

        private int healthPoints = 100;
        
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