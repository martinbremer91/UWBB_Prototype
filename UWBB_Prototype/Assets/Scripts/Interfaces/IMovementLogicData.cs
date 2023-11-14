using UnityEngine;

namespace UWBB.Interfaces
{
    public interface IMovementLogicData : IPlayerLogicData
    {
        public Vector3 movementVector { get; set; }
    }
}