using System;
using Player.Movement;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : IFixedTickable, IPlayerMovement
    {
        private IPlayerMovable _moveBehaviour;

        private PlayerMovement(Player player, Settings settings) => 
            SetMoveBehaviour(new PlayerMovableMoveBehaviour(player, settings));

        public void FixedTick() => _moveBehaviour.Move();

        public void SetMoveBehaviour(IPlayerMovable moveBehaviour) => _moveBehaviour = moveBehaviour;

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float MovementSpeed { get; private set; }   
        }
    }

    public interface IPlayerMovement
    {
        void SetMoveBehaviour(IPlayerMovable moveBehaviour);
    }
}