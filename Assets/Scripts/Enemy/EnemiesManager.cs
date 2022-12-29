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

        private readonly Utils.IFactory<Enemy> _factory;
        private readonly ISceneBoundsService _service;
        private readonly Settings _settings;
        private float _currentSpawnDelay;

        private EnemiesManager(Utils.IFactory<Enemy> factory, ISceneBoundsService service, Settings settings)
        {
            _factory = factory;
            _service = service;
            _settings = settings;
        }

        public void Tick()
        {
            if (IsContinuing())
                return;

            Spawn();
            _currentSpawnDelay = _settings.SpawnDelay;
        }

        private bool IsContinuing()
        {
            if (Enemies.Count < _settings.StartCount)
                return false;

            if (Enemies.Count >= _settings.MaxCount)
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
            enemy.Presenter.OnDestroyed += () => OnEnemyPresenterDestroyed(enemy);
            enemy.Presenter.LocalScale = EnemiesManagerData.GetRandomLocalScale(_settings.MinScale, _settings.MaxScale);
            enemy.Presenter.Position =
                EnemiesManagerData.GetRandomEnemyPosition(enemy.Presenter.LocalScale.x, _service);
            enemy.Presenter.Velocity = EnemiesManagerData.GetRandomEnemyVelocity() * _settings.StartVelocity;
            enemy.Presenter.Drag = _settings.Drag;
            Enemies.Add(enemy);
        }

        private void OnEnemyPresenterDestroyed(Enemy enemy) => Enemies.Remove(enemy);

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Material Material { get; private set; }
            [field: SerializeField] public Mesh Mesh { get; private set; }
            [field: SerializeField] public float MinScale { get; private set; }
            [field: SerializeField] public float MaxScale { get; private set; }
            [field: SerializeField] public float SpawnDelay { get; private set; }
            [field: SerializeField] public int StartCount { get; private set; }
            [field: SerializeField] public int MaxCount { get; private set; }
            [field: SerializeField] public float StartVelocity { get; private set; }
            [field: SerializeField] public float Drag { get; private set; }
        }
    }

    public interface IEnemiesManager
    {
        List<Enemy> Enemies { get; }
    }
}