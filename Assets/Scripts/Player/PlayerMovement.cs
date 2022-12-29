using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : ITickable
    {
        private readonly Player _player;
        private readonly Settings _settings;

        private PlayerMovement(Player player, Settings settings)
        {
            _player = player;
            _settings = settings;
        }
        
        public void Tick()
        {
            if(PlayerInput.IsMovingLeft)
                _player.Presenter.AddForce(Vector3.left * _settings.MovementSpeed);
            
            if(PlayerInput.IsMovingRight)
                _player.Presenter.AddForce(Vector3.right * _settings.MovementSpeed);
            
            if(PlayerInput.IsMovingUp)
                _player.Presenter.AddForce(Vector3.up * _settings.MovementSpeed);
            
            if(PlayerInput.IsMovingDown)
                _player.Presenter.AddForce(Vector3.down * _settings.MovementSpeed);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float MovementSpeed { get; private set; }   
        }
    }
}