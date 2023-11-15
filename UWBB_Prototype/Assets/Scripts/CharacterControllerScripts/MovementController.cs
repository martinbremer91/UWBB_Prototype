using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class MovementController : IInitializable<Player>
    {
        private Transform player;
        private Transform playerModel;
        
        public void Init(Player p)
        {
            player = p.transform;
            playerModel = p.playerModel;
        }
        
        public void ProcessMovementData(IMovementLogicData movementData)
        {
            // TODO: switch from translation to add force
            Vector3 movementVector = movementData.movementVector;
            player.Translate(movementVector, Space.World);
            playerModel.LookAt(player.position + movementVector);
        }
    }
}