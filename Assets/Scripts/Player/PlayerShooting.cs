using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerShooting : ITickable
    {
        private readonly Player _player;
        private readonly Settings _settings;
        private float _bulletsShootingDelay;
        private float _extraShootingDelay;
        private float _extraShootingReloadDelay;
        private int _extraShootingCount;

        private PlayerShooting(Player player, Settings settings)
        {
            _player = player;
            _settings = settings;
            _extraShootingCount = _settings.ExtraShootingMaxCount;
        }
        
        public void Tick()
        {
            Continue();
            
            if(PlayerInput.IsShootingBullets && _bulletsShootingDelay <= 0)
                ShootBullet();
            
            if(PlayerInput.IsShootingExtra && _extraShootingDelay <= 0)
                ShootExtra();

            void Continue()
            {
                if(_bulletsShootingDelay > 0)
                    _bulletsShootingDelay -= Time.deltaTime;
                
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
        }

        private void ShootBullet()
        {
            if (_bulletsShootingDelay > 0)
                return;

            SpawnBullet();
            _bulletsShootingDelay = _settings.BulletsShootingDelay;
        }

        private void ShootExtra()
        {
            if (_extraShootingDelay > 0 || _extraShootingCount <= 0)
                return;

            _extraShootingDelay = _settings.ExtraShootingDelay;
            _extraShootingCount--;
        }

        private void SpawnBullet(Vector3 offset = default)
        {
            var spawnPoint = _player.Presenter.BulletsSpawnPoint;
            
            if(!spawnPoint)
                return;
            
            
        }
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float BulletsShootingDelay { get; private set; }   
            [field: SerializeField] public float ExtraShootingDelay { get; private set; } 
            [field: SerializeField] public float ExtraShootingReloadDelay { get; private set; }
            [field: SerializeField] public int ExtraShootingMaxCount { get; private set; }
        }
    }
}