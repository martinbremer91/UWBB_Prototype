using UnityEngine;

namespace UWBB.Interfaces
{
    public interface ICameraLogicData : IPlayerLogicData
    {
        public Vector3 pivotPoint { get; }
        public Vector3 rotationXAxis { get; }
        public float angleX { get; }
        public Vector3 rotationYAxis { get; }
        public float angleY { get; }
    }
}