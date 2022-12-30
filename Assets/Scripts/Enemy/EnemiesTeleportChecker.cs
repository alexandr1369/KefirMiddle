using Location;
using UnityEngine;
using Utils;
using Zenject;

namespace Enemy
{
    public class EnemiesTeleportChecker : ITickable
    {
        private readonly IEnemiesManager _manager;
        private readonly ISceneBoundsService _sceneBoundsService;
        private float _currentDelay;

        private EnemiesTeleportChecker(IEnemiesManager manager, ISceneBoundsService sceneBoundsService)
        {
            _manager = manager;
            _sceneBoundsService = sceneBoundsService;
        }

        public void Tick()
        {
            if (_currentDelay > 0)
            {
                _currentDelay -= Time.deltaTime;
                return;
            }
            
            _currentDelay = _manager.ManagerSettings.TeleportCheckerDelay;
            _manager.Enemies.ForEach(CheckForTeleport);
        }

        private void CheckForTeleport(Enemy enemy)
        {
            var presenter = enemy.Presenter;
            var scale = presenter.LocalScale.x;
            
            if (presenter.Position.x > _sceneBoundsService.Right + scale && IsMovingInDirection(enemy, Vector3.right))
            {
                presenter.Transform.SetX(_sceneBoundsService.Left - scale);
            }
            else if (presenter.Position.x < _sceneBoundsService.Left - scale && IsMovingInDirection(enemy, -Vector3.right))
            {
                presenter.Transform.SetX(_sceneBoundsService.Right + scale);
            }
            else if (presenter.Position.y < _sceneBoundsService.Bottom - scale && IsMovingInDirection(enemy, -Vector3.up))
            {
                presenter.Transform.SetY(_sceneBoundsService.Top + scale);
            }
            else if (presenter.Position.y > _sceneBoundsService.Top + scale && IsMovingInDirection(enemy, Vector3.up))
            {
                presenter.Transform.SetY(_sceneBoundsService.Bottom - scale);
            }
        }
        
        private bool IsMovingInDirection(Enemy enemy, Vector3 dir) => 
            Vector3.Dot(dir, enemy.Presenter.Velocity) > 0;
    }
}