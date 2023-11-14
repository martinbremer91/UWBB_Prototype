using UnityEngine;

namespace UWBB.Interfaces
{
    public interface ICameraLogicData : IPlayerLogicData
    {
        public Vector3 pivotPoint { get; set; }
        public Vector3 rotationXAxis { get; set; }
        public float angleX { get; set; }
        public Vector3 rotationYAxis { get; set; }
        public float angleY { get; set; }
    }
}