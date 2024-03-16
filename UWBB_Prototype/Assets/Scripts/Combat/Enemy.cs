using UnityEngine;
using UWBB.CharacterController;
using UWBB.Interfaces;

namespace UWBB.Combat
{
    public class Enemy : MonoBehaviour, IDamageable, IKnockbackable, ILockOnTarget
    {
        public static Player player;
        
        public Vector3 position => transform.position;
        public GameObject lockTarget => gameObject;
        public IDamageable damageable => this;
        public IKnockbackable knockbackable => this;

        public int currentHealthPoints { get; set; }
        public int totalHealthPoints => 500;

        private void Start() 
            => player.lockOnController.lockOnTargets.Add(this);

        public void TakeDamage(int value)
        {
            currentHealthPoints -= value;
            Debug.Log(currentHealthPoints);
        }

        public void TakeKnockback(Vector3 direction, float value)
        {
            transform.position += direction * value;
            Debug.Log("knockback: " + value);
        }
    }
}