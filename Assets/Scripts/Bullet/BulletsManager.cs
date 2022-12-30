using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Bullet
{
    public class BulletsManager : ITickable, IBulletsManager
    {
        public List<Bullet> Bullets { get; } = new();
        
        private readonly Settings _settings;

        private BulletsManager(Settings settings)
        {
            _settings = settings;
        }

        public void Tick()
        {
            
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Material Material { get; private set; }
            [field: SerializeField] public Mesh Mesh { get; private set; }
            [field: SerializeField] public float TeleportCheckerDelay { get; private set; }
            [field: SerializeField] public float StartVelocity { get; private set; }
            [field: SerializeField] public float Drag { get; private set; }
        }
    }
}