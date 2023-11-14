﻿using UnityEngine;
using UnityEngine.Serialization;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        private PlayerLogicManager playerLogicManager = new();
        
        public IMovementLogicData movementData;
        public ICameraLogicData cameraData;
        public ILockOnLogicData lockOnData;
        
        private readonly MovementController movementController = new();
        private readonly CameraController cameraController = new();
        private readonly LockOnController lockOnController = new();

        public Transform playerModel;
        public Transform cameraTransform;

        public void Init()
        {
            playerLogicManager.Init(this);
            cameraController.Init(this);
        }

        private void Update()
        {
            playerLogicManager.RunLogicUpdate();
            
            movementController.ProcessMovementData(movementData);
            lockOnController.ProcessLockOnData(lockOnData);
        }

        private void LateUpdate() => cameraController.ProcessCameraData(cameraData);
    }
}