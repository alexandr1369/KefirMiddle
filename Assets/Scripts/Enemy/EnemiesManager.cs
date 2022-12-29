using System;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemiesManager : ITickable
    {
        private readonly Utils.IFactory<Enemy> _factory;
        private readonly Settings _settings;

        private EnemiesManager(Utils.IFactory<Enemy> factory, Settings settings)
        {
            _factory = factory;
            _settings = settings;
        }
        
        public void Tick()
        {
            Debug.Log("Ticking enemy manager");
        }
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Material Material { get; private set; }
            [field: SerializeField] public Mesh Mesh { get; private set; }
            [field: SerializeField] public float MinScale { get; private set; }
            [field: SerializeField] public float MaxScale { get; private set; }
            [field: SerializeField] public int MaxCount { get; private set; }
        }
    }
}