using System;
using UnityEngine;
using Zenject;

namespace Player.Shooting
{
    public class TwoBulletsShootBehaviour : ITickable, IShooting
    {
        private readonly Core _core;
        private readonly PlayerShooting.Settings _settings;
        private float _extraShootingDelay;
        private float _extraShootingReloadDelay;
        private int _extraShootingCount;

        public TwoBulletsShootBehaviour(Core core, PlayerShooting.Settings settings)
        {
            _core = core;
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
            
            SpawnBullet();
            SpawnBullet();

            _extraShootingDelay = _settings.ExtraShootingDelay;
            _extraShootingCount--;
        }

        private void SpawnBullet(Vector3 offset = default)
        {
            var spawnPoint = _core.Presenter.BulletsSpawnPoint;
            
            if(!spawnPoint)
                return;
            
            
        }
    }
}