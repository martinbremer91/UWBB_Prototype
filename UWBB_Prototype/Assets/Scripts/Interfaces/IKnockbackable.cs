using UnityEngine;

namespace DefaultNamespace
{
    public interface IKnockbackable
    {
        void TakeKnockback(Vector3 direction, float value);
    }
}