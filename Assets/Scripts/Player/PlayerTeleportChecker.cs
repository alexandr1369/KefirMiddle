using Location;
using UnityEngine;
using Utils;
using Zenject;

namespace Player
{
    public class PlayerTeleportChecker : ITickable
    {
        private readonly ISceneBoundsService _sceneBoundsService;
        private readonly Player _player;

        private PlayerTeleportChecker(Player player, ISceneBoundsService sceneBoundsService)
        {
            _sceneBoundsService = sceneBoundsService;
            _player = player;
        }

        public void Tick() => CheckForTeleport();

        private void CheckForTeleport()
        {
            var presenter = _player.Presenter;
            var scale = presenter.LocalScale.x;
            
            if (presenter.Position.x > _sceneBoundsService.Right + scale && IsMovingInDirection(Vector3.right))
            {
                presenter.Transform.SetX(_sceneBoundsService.Left - scale);
            }
            else if (presenter.Position.x < _sceneBoundsService.Left - scale && IsMovingInDirection(-Vector3.right))
            {
                presenter.Transform.SetX(_sceneBoundsService.Right + scale);
            }
            else if (presenter.Position.y < _sceneBoundsService.Bottom - scale && IsMovingInDirection(-Vector3.up))
            {
                presenter.Transform.SetY(_sceneBoundsService.Top + scale);
            }
            else if (presenter.Position.y > _sceneBoundsService.Top + scale && IsMovingInDirection(Vector3.up))
            {
                presenter.Transform.SetY(_sceneBoundsService.Bottom - scale);
            }
        }
        
        private bool IsMovingInDirection(Vector3 dir) => 
            Vector3.Dot(dir, _player.Presenter.Velocity) > 0;
    }
}