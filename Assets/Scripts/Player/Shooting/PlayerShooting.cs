using System;
using System.Collections.Generic;
using Bullet;
using LoadingSystem.Loading.Operations.Home;
using Player.Shooting.ExtraShooting;
using UnityEngine;
using Utils;
using Zenject;

namespace Player.Shooting
{
    public class PlayerShooting : ITickable, IPlayerShooting
    {
        private readonly Core _core;
        private readonly IBulletsService _bulletsService;
        private readonly Settings _settings;
        private readonly IInputDelegate _inputDelegate;
        private readonly HomeSceneLoadingContext _context;
        private readonly List<ITickable> _tickables = new();
        private readonly List<IShooting> _shootBehaviours = new();
        private bool _isActive;

        private PlayerShooting(Core core, IBulletsService bulletsService, Settings settings, 
            HomeSceneLoadingContext context, IInputDelegate inputDelegate)
        {
            _core = core;
            _bulletsService = bulletsService;
            _settings = settings;
            _inputDelegate = inputDelegate;
            _context = context;
            _context.PlayerShooting = this;
            
            InitShootBehaviours();
        }

        private void InitShootBehaviours()
        {
            var shootBehaviour1 = new SingleBulletShootBehaviour(_core, _bulletsService, _settings);
            var shootBehaviour2 = new TwoBulletsShootBehaviour(_core, _bulletsService, _settings, _context);
            
            _shootBehaviours.Add(shootBehaviour1);
            _shootBehaviours.Add(shootBehaviour2);
            _tickables.Add(shootBehaviour1);
            _tickables.Add(shootBehaviour2);
        }

        public void Start() => _isActive = true;

        public void Stop() => _isActive = false;

        public void Tick()
        {
            if (!_inputDelegate.HasPermission(this)
                || !_isActive
                || _core.Presenter.IsDead)
            {
                return;
            }

            _tickables.ForEach(tickable => tickable.Tick());
            if(PlayerInput.IsShootingBullets)
                _shootBehaviours[0].Shoot();
            
            if(PlayerInput.IsShootingExtra)
                _shootBehaviours[1].Shoot();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float BulletsShootingDelay { get; private set; }   
            [field: SerializeField] public float ExtraShootingDelay { get; private set; } 
            [field: SerializeField] public float ExtraShootingReloadDelay { get; private set; }
            [field: SerializeField] public float ExtraShootingDistanceMultiplier { get; private set; }
            [field: SerializeField] public int ExtraShootingMaxCount { get; private set; }
        }
    }
}