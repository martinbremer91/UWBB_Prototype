using UnityEngine;

namespace UWBB.Interfaces
{
    public interface ILockOnTarget
    {
        public Vector3 position { get; }
        
        // temp
        public GameObject go { get; }
    }
}
