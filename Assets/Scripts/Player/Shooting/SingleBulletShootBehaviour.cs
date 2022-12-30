using UnityEngine;
using Zenject;

namespace Player.Shooting
{
    public class SingleBulletShootBehaviour : ITickable, IShooting
    {
        private readonly Core _core;
        private readonly PlayerShooting.Settings _settings;
        private float _bulletsShootingDelay;

        public SingleBulletShootBehaviour(Core core, PlayerShooting.Settings settings)
        {
            _core = core;
            _settings = settings;
        }

        public void Tick()
        {
            if(_bulletsShootingDelay > 0)
                _bulletsShootingDelay -= Time.deltaTime;
        }

        public void Shoot()
        {
            if (_bulletsShootingDelay > 0)
                return;

            SpawnBullet();
            
            _bulletsShootingDelay = _settings.BulletsShootingDelay;
        }

        private void SpawnBullet()
        {
            var spawnPoint = _core.Presenter.BulletsSpawnPoint;
            
            if(!spawnPoint)
                return;
            
            
        }
    }
}