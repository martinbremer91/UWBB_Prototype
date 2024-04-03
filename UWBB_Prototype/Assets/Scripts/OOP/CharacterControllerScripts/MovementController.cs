using UnityEngine;
using UWBB.GameFramework;

namespace UWBB.CharacterController
{
    public class MovementController
    {
        private CharacterControllerConfigs configs;
        
        private Transform player;
        private Transform playerModel;

        private bool dashing;
        private const float dashDuration = 0.5f;
        private const float dashSpeedMultiplier = 3;
        private float dashTimer;
        
        public void Init(Player p)
        {
            configs = Main.instance.configs.ccConfigs;
            player = p.transform;
            playerModel = p.playerModel;
        }
        
        public void ProcessMovementData(PlayerMovementData movementData)
        {
            // TODO: switch from translation to add force
            Vector3 movementVector = HandleDash(movementData);
            
            player.Translate(movementVector * (configs.speed * Time.deltaTime), Space.World);
            playerModel.LookAt(player.position + movementVector);
        }

        private Vector3 HandleDash(PlayerMovementData movementData)
        {
            Vector3 movementVector = movementData.movementVector;
            
            if (movementData.dashCommand && !dashing)
            {
                dashing = true;
                dashTimer = 0;
            }

            if (dashing)
            {
                dashTimer += Time.deltaTime;
                if (dashTimer >= dashDuration)
                    dashing = false;

                movementVector *= dashSpeedMultiplier;
            }

            return movementVector;
        }
    }
}