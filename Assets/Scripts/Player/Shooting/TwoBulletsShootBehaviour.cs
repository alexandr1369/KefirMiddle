using System;
using Bullet;
using UnityEngine;
using Zenject;

namespace Player.Shooting
{
    public class TwoBulletsShootBehaviour : ITickable, IShooting
    {
        private readonly Core _core;
        private readonly IBulletsService _bulletsService;
        private readonly PlayerShooting.Settings _settings;
        private float _extraShootingDelay;
        private float _extraShootingReloadDelay;
        private int _extraShootingCount;

        public TwoBulletsShootBehaviour(Core core, IBulletsService bulletsService, PlayerShooting.Settings settings)
        {
            _core = core;
            _bulletsService = bulletsService;
            _settings = settings;
            _extraShootingCount = _settings.ExtraShootingMaxCount;
        }

        public void Tick()
        {
            if(_extraShootingDelay > 0)
                _extraShootingDelay -= Time.deltaTime;

            if (_extraShootingCount >= _settings.ExtraShootingMaxCount) 
                return;
                
            _extraShootingReloadDelay -= Time.deltaTime;

            if (_extraShootingReloadDelay > 0) 
                return;
                    
            _extraShootingReloadDelay = _settings.ExtraShootingReloadDelay;
            _extraShootingCount = Math.Clamp(_extraShootingCount + 1, 0, _settings.ExtraShootingMaxCount);
        }

        public void Shoot()
        {
            if (_extraShootingDelay > 0 || _extraShootingCount <= 0)
                return;
            
            var transformUp = _core.Presenter.Transform.up;
            var shootingDistanceMultiplier = _settings.ExtraShootingDistanceMultiplier;
            var offset = transformUp * shootingDistanceMultiplier;
            
            SpawnBullet(_core.Presenter.Direction + offset);
            SpawnBullet(_core.Presenter.Direction - offset);

            _extraShootingDelay = _settings.ExtraShootingDelay;
            _extraShootingCount--;
        }

        private void SpawnBullet(Vector3 offset = default)
        {
            var spawnPoint = _core.Presenter.BulletsSpawnPoint;
            
            if(!spawnPoint)
                return;
            
            _bulletsService.SpawnBulletAt(spawnPoint.position + offset, _core.Presenter.Direction);
        }
    }
}