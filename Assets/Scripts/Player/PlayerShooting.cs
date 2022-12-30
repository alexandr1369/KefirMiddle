using System;
using System.Collections.Generic;
using Player.Shooting;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerShooting : ITickable
    {
        private readonly Core _core;
        private readonly Settings _settings;
        private readonly List<ITickable> _tickables = new();
        private readonly List<IShooting> _shootBehaviours = new();

        private PlayerShooting(Core core, Settings settings)
        {
            _core = core;
            _settings = settings;
            
            InitShootBehaviours();
        }

        private void InitShootBehaviours()
        {
            var shootBehaviour1 = new SingleBulletShootBehaviour(_core, _settings);
            var shootBehaviour2 = new TwoBulletsShootBehaviour(_core, _settings);
            
            _shootBehaviours.Add(shootBehaviour1);
            _shootBehaviours.Add(shootBehaviour2);
            _tickables.Add(shootBehaviour1);
            _tickables.Add(shootBehaviour2);
        }

        public void Tick()
        {
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
            [field: SerializeField] public int ExtraShootingMaxCount { get; private set; }
        }
    }
}