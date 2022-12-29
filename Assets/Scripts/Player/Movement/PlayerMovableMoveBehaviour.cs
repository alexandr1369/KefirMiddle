using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovableMoveBehaviour : IPlayerMovable
    {
        private readonly Player _player;
        private readonly PlayerMovement.Settings _settings;

        public PlayerMovableMoveBehaviour(Player player, PlayerMovement.Settings settings)
        {
            _player = player;
            _settings = settings;
        }

        public void Move()
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
    }
}