using Location;
using UnityEngine;
using Zenject;

namespace Enemy
{
    
    public class EnemiesTeleportChecker : ITickable
    {
        private readonly IEnemiesManager _manager;
        private readonly ITeleportCheckService _teleportCheckService;
        private float _currentDelay;

        private EnemiesTeleportChecker(IEnemiesManager manager, ITeleportCheckService teleportCheckService)
        {
            _manager = manager;
            _teleportCheckService = teleportCheckService;
        }

        public void Tick()
        {
            if (_currentDelay > 0)
            {
                _currentDelay -= Time.deltaTime;
                return;
            }
            
            _currentDelay = _manager.ManagerSettings.TeleportCheckerDelay;
            _manager.Enemies.ForEach(enemy => _teleportCheckService.CheckForTeleport(enemy.Presenter));
        }
    }
}