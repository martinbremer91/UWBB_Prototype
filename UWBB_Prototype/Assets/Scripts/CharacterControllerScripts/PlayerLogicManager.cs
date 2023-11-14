using System;
using MBre.Utilities;
using UWBB.CharacterController.Abzu;
using UWBB.CharacterController.FirstVersion;
using UWBB.GameFramework;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class PlayerLogicManager : IInitializable<Player>
    {
        private Player player;
        private CharacterControllerConfigs ccConfigsBf;
        private CharacterControllerConfigs ccConfigs
        {
            get
            {
                if (ccConfigsBf == null)
                    ccConfigsBf = Main.instance.configs.ccConfigs;
                return ccConfigsBf;
            }
        }

        private IInputLogic<IInputState> inputLogic;
        private IPlayerLogic<IInputState, IMovementLogicData> movementLogic;
        private IPlayerLogic<IInputState, ICameraLogicData> cameraLogic;
        private IPlayerLogic<IInputState, ILockOnLogicData> lockOnLogic;

        private MovementLogicType movementType;
        private LockOnLogicType lockOnType;

        private bool invalidMovementLogic => movementType != ccConfigs.activeMovementType;
        private bool invalidLockOnLogic => lockOnType != ccConfigs.activeLockOnType;
        
        public void Init(Player p)
        {
            player = p;
            ValidatePlayerLogicRefs();
        }
        
        public void RunLogicUpdate()
        {
            ValidatePlayerLogicRefs(invalidMovementLogic, invalidLockOnLogic);
            
            IInputState inputState = inputLogic.GetInputState();
            player.movementData = movementLogic.RunUpdate(inputState);
            DebugPanel.CustomDebug($"Movement: " + player.movementData.movementVector);
            // player.cameraData = cameraLogic.RunUpdate(inputState);
            // player.lockOnData = lockOnLogic.RunUpdate(inputState);
        }
        
        private void ValidatePlayerLogicRefs(bool updateMovementType = true, bool updateLockOnType = true)
        {
            if (updateMovementType)
            {
                movementType = ccConfigs.activeMovementType;
                SetInputLogicRef(movementType);
                SetMovementLogicRef(movementType);
                SetCameraLogicRef(movementType);
            }
            
            if (updateLockOnType)
            {
                lockOnType = ccConfigs.activeLockOnType;
                SetLockOnLogicRef(lockOnType);
            }
        }

        private void SetInputLogicRef(MovementLogicType movement)
        {
            if (inputLogic != null)
                inputLogic.Deinit();
            
            inputLogic = movement switch
            {
                MovementLogicType.FirstVersion => new FirstVersionInputLogic(),
                MovementLogicType.Abzu => new AbzuInputLogic(),
                _ => throw new ArgumentOutOfRangeException(),
            };
            
            inputLogic.Init();
        }
        
        private void SetMovementLogicRef(MovementLogicType movement)
        {
            movementLogic = movement switch
            {
                MovementLogicType.FirstVersion => new FirstVersionMovementLogic(),
                MovementLogicType.Abzu => new AbzuMovementLogic(),
                _ => throw new ArgumentOutOfRangeException(),
            };
            
            movementLogic.Init(player);
        }
        
        private void SetCameraLogicRef(MovementLogicType movement)
        {
            cameraLogic = movement switch
            {
                MovementLogicType.FirstVersion => new FirstVersionCameraLogic(),
                MovementLogicType.Abzu => new AbzuCameraLogic(),
                _ => throw new ArgumentOutOfRangeException(),
            };
            
            cameraLogic.Init(player);
        }

        private void SetLockOnLogicRef(LockOnLogicType lockOn)
        {
            lockOnLogic = lockOn switch
            {
                LockOnLogicType.FirstVersion => new FirstVersionLockOnLogic(),
                LockOnLogicType.Abzu => new AbzuLockOnLogic(),
                _ => throw new ArgumentOutOfRangeException(),
            };
            
            lockOnLogic.Init(player);
        }
    }
}