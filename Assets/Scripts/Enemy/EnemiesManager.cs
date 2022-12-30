using System;
using System.Collections.Generic;
using Location;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemiesManager : ITickable, IEnemiesManager
    {
        public List<Enemy> Enemies { get; } = new();
        public Settings ManagerSettings { get; }

        private readonly Utils.IFactory<Enemy> _factory;
        private readonly ISceneBoundsService _service;
        private float _currentSpawnDelay;
        private bool _isActive;

        private EnemiesManager(Utils.IFactory<Enemy> factory, ISceneBoundsService service, Settings settings)
        {
            _factory = factory;
            _service = service;
            ManagerSettings = settings;
            
            // TODO: move to operation
            Start();
        }

        public void Start() => _isActive = true;

        public void Stop() => _isActive = false;

        public void Tick()
        {
            if (IsContinuing())
                return;

            Spawn();
            _currentSpawnDelay = ManagerSettings.SpawnDelay;
        }

        private bool IsContinuing()
        {
            if(!_isActive)
                return true;
            
            if (Enemies.Count < ManagerSettings.StartCount)
                return false;

            if (Enemies.Count >= ManagerSettings.MaxCount)
                return true;

            if (_currentSpawnDelay > 0)
            {
                _currentSpawnDelay -= Time.deltaTime;

                return true;
            }

            return false;
        }

        private void Spawn()
        {
            var enemy = _factory.Create();
            enemy.Init(null);
            enemy.Presenter.OnDestroyed += () => OnPresenterDestroyed(enemy);
            enemy.Presenter.LocalScale = EnemiesManagerData.GetRandomLocalScale(ManagerSettings.MinScale, ManagerSettings.MaxScale);
            enemy.Presenter.Position =
                EnemiesManagerData.GetRandomEnemyPosition(enemy.Presenter.LocalScale.x, _service);
            enemy.Presenter.Velocity = EnemiesManagerData.GetRandomEnemyVelocity() * ManagerSettings.StartVelocity;
            enemy.Presenter.Drag = ManagerSettings.Drag;
            Enemies.Add(enemy);
        }

        private void OnPresenterDestroyed(Enemy enemy) => Enemies.Remove(enemy);

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Material Material { get; private set; }
            [field: SerializeField] public Mesh Mesh { get; private set; }
            [field: SerializeField] public float MinScale { get; private set; }
            [field: SerializeField] public float MaxScale { get; private set; }
            [field: SerializeField] public float SpawnDelay { get; private set; }
            [field: SerializeField] public float TeleportCheckerDelay { get; private set; }
            [field: SerializeField] public int StartCount { get; private set; }
            [field: SerializeField] public int MaxCount { get; private set; }
            [field: SerializeField] public float StartVelocity { get; private set; }
            [field: SerializeField] public float Drag { get; private set; }
        }
    }
}