using Player;
using Player.Movement;
using Presenter;
using UnityEngine;

namespace Movement
{
    public class PlayerMoveBehaviour : IMovable
    {
        private readonly Core _core;
        private readonly PlayerMovement.Settings _settings;

        public PlayerMoveBehaviour(Core core, PlayerMovement.Settings settings)
        {
            _core = core;
            _settings = settings;
        }

        public void Move()
        {
            if(PlayerInput.IsMovingLeft)
                _core.Presenter.AddForce(Vector3.left * _settings.MovementSpeed);
            
            if(PlayerInput.IsMovingRight)
                _core.Presenter.AddForce(Vector3.right * _settings.MovementSpeed);
            
            if(PlayerInput.IsMovingUp)
                _core.Presenter.AddForce(Vector3.up * _settings.MovementSpeed);
            
            if(PlayerInput.IsMovingDown)
                _core.Presenter.AddForce(Vector3.down * _settings.MovementSpeed);
        }
    }
}