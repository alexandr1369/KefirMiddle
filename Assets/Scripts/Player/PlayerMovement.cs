using System;
using Movement;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : IFixedTickable, IPlayerMovement
    {
        private IMovable _moveBehaviour;

        private PlayerMovement(Player player, Settings settings) => 
            SetMoveBehaviour(new MovableMoveBehaviour(player, settings));

        public void FixedTick() => _moveBehaviour.Move();

        public void SetMoveBehaviour(IMovable moveBehaviour) => _moveBehaviour = moveBehaviour;

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float MovementSpeed { get; private set; }   
        }
    }

    public interface IPlayerMovement
    {
        void SetMoveBehaviour(IMovable moveBehaviour);
    }
}