using UnityEngine;
using DefaultNamespace;

public class AttackTrigger : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public float knockback;

    private void OnTriggerEnter(Collider other)
    {
        IAttackable attackable = other.GetComponent<IAttackable>();
        
        if (attackable != null)
        {
            attackable.damageable.TakeDamage(damage);
            attackable.knockbackable.TakeKnockback(Vector3.forward, knockback);
        }
    }
}