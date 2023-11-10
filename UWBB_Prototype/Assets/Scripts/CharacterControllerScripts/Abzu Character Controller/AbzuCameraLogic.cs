﻿using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuCameraLogic : 
        IPlayerLogic<IInputState, ICameraLogicData>,
        IPlayerLogic<AbzuInputState, AbzuCameraData>
    {
        public ICameraLogicData RunUpdate(IInputState inputState) 
            => RunUpdate((AbzuInputState)inputState);
        
        public AbzuCameraData RunUpdate(AbzuInputState inputState)
        {
            Debug.Log("AbzuCamera RunUpdate");
            return default;
        }
    }
    
    public struct AbzuCameraData : ICameraLogicData {}
}