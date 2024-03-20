using UnityEngine;

namespace UWBB.Interfaces
{
    public interface ILockOnLogicData : IPlayerLogicData
    {
        public bool lockedOn { get; }
        public ILockOnTarget target { get; }
    }
}