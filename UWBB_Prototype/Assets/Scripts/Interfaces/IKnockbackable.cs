using UnityEngine;

namespace UWBB.Interfaces
{
    public interface IKnockbackable
    {
        void TakeKnockback(Vector3 direction, float value);
    }
}