using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuCameraLogic : 
        IPlayerLogic<IInputState, ICameraLogicData>,
        IPlayerLogic<AbzuInputState, AbzuCameraData>
    {
        public void Init(Player player)
        {
            
        }
        
        public ICameraLogicData RunUpdate(IInputState inputState) 
            => RunUpdate((AbzuInputState)inputState);
        
        public AbzuCameraData RunUpdate(AbzuInputState inputState)
        {
            Debug.Log("AbzuCamera RunUpdate");
            return default;
        }
    }
    
    public struct AbzuCameraData : ICameraLogicData
    {
        public Vector3 pivotPoint { get; set; }
        public Vector3 rotationXAxis { get; set; }
        public float angleX { get; set; }
        public Vector3 rotationYAxis { get; set; }
        public float angleY { get; set; }
    }
}