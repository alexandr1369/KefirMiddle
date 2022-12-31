using Bullet;
using LoadingSystem.Loading.Operations.Home;
using UnityEngine;
using Zenject;

namespace Player.Shooting
{
    public class SingleBulletShootBehaviour : ITickable, IShooting
    {
        private readonly Core _core;
        private readonly IBulletsService _bulletsService;
        private readonly PlayerShooting.Settings _settings;
        private readonly HomeSceneLoadingContext _context;
        private float _bulletsShootingDelay;

        public SingleBulletShootBehaviour(Core core, IBulletsService bulletsService, PlayerShooting.Settings settings,
            HomeSceneLoadingContext context)
        {
            _core = core;
            _bulletsService = bulletsService;
            _settings = settings;
            _context = context;
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
            
            _bulletsService.SpawnBulletAt(spawnPoint.position, _core.Presenter.Direction);
            _context.AudioService.PlayLocalFx(_core.Presenter.AudioSource, _context.AudioService.ShootClip);
        }
    }
}