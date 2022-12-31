using System;
using Bullet;
using LoadingSystem.Loading.Operations.Home;
using UnityEngine;
using Zenject;

namespace Player.Shooting.ExtraShooting
{
    public class TwoBulletsShootBehaviour : ITickable, IShooting, IPlayerExtraShootingAdapter
    {
        public float ReloadDelay { get; private set; }
        public int Count { get; private set; }
        
        private readonly Core _core;
        private readonly IBulletsService _bulletsService;
        private readonly PlayerShooting.Settings _settings;
        private readonly HomeSceneLoadingContext _context;
        private float _extraShootingDelay;

        public TwoBulletsShootBehaviour(Core core, IBulletsService bulletsService, PlayerShooting.Settings settings,
            HomeSceneLoadingContext context)
        {
            _core = core;
            _bulletsService = bulletsService;
            _settings = settings;
            _context = context;
            _context.PlayerExtraShootingAdapter = this;
            Count = _settings.ExtraShootingMaxCount;
        }

        public void Tick()
        {
            if (_extraShootingDelay > 0) 
                _extraShootingDelay -= Time.deltaTime;

            if (Count >= _settings.ExtraShootingMaxCount)
            {
                ReloadDelay = 0;
                return;
            } 
                
            ReloadDelay -= Time.deltaTime;
            
            if (ReloadDelay > 0) 
                return;
                    
            ReloadDelay = _settings.ExtraShootingReloadDelay;
            Count = Math.Clamp(Count + 1, 0, _settings.ExtraShootingMaxCount);
        }

        public void Shoot()
        {
            if (_extraShootingDelay > 0 || Count <= 0)
                return;
            
            var transformUp = _core.Presenter.Transform.up;
            var shootingDistanceMultiplier = _settings.ExtraShootingDistanceMultiplier;
            var offset = transformUp * shootingDistanceMultiplier;
            
            SpawnBullet(_core.Presenter.Direction + offset);
            SpawnBullet(_core.Presenter.Direction - offset);

            if(ReloadDelay <= 0)
                ReloadDelay = _settings.ExtraShootingReloadDelay;
            
            _extraShootingDelay = _settings.ExtraShootingDelay;
            Count--;
        }

        private void SpawnBullet(Vector3 offset = default)
        {
            var spawnPoint = _core.Presenter.BulletsSpawnPoint;
            
            if(!spawnPoint)
                return;
            
            _bulletsService.SpawnBulletAt(spawnPoint.position + offset, _core.Presenter.Direction);
            _context.AudioService.PlayLocalFx(_core.Presenter.AudioSource, _context.AudioService.ShootClip);
        }
    }
}