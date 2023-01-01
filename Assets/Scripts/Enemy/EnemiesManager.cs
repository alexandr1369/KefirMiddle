using System;
using System.Collections.Generic;
using System.Linq;
using LoadingSystem.Loading.Operations.Home;
using Location;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemiesManager : IFixedTickable, ITickable, IEnemiesManager
    {
        public List<Enemy> Enemies { get; } = new();
        public Settings ManagerSettings { get; }

        private readonly Factory.IFactory<Enemy> _factory;
        private readonly ISceneBoundsService _service;
        private float _currentSpawnDelay;
        private bool _isActive;

        private EnemiesManager(Factory.IFactory<Enemy> factory, ISceneBoundsService service, Settings settings,
            HomeSceneLoadingContext context)
        {
            _factory = factory;
            _service = service;
            ManagerSettings = settings;
            context.EnemiesManager = this;
        }

        public void Start() => _isActive = true;

        public void Stop() => _isActive = false;

        public void FixedTick() => Enemies.ForEach(enemy => ((IFixedTickable)enemy).FixedTick());

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

        private void Spawn(IEnemyTypeAdapter.Type? type = null, Vector3? position = null, Vector3? velocity = null)
        {
            var enemy = _factory.Create();
            var enemyType = type ?? GetEnemyType();
            var typeSettings = ManagerSettings.TypeSettings.Find(settings => settings.Type == enemyType);
            enemy.Init(null);
            ((IEnemyTypeAdapter)enemy).SetEnemyType(enemyType, velocity);
            enemy.Presenter.OnDestroyedTemporary += OnPresenterDestroyed;
            enemy.Presenter.LocalScale = 
                EnemiesManagerData.GetRandomLocalScale(typeSettings.MinScale, typeSettings.MaxScale);
            enemy.Presenter.Position = position ??
                EnemiesManagerData.GetRandomEnemyPosition(enemy.Presenter.LocalScale.x, _service);
            enemy.Presenter.Drag = ManagerSettings.Drag;
            Enemies.Add(enemy);

            void OnPresenterDestroyed()
            {
                Enemies.Remove(enemy);

                if (((IEnemyTypeAdapter)enemy).EnemyType != IEnemyTypeAdapter.Type.Asteroid) 
                    return;
                
                Spawn(IEnemyTypeAdapter.Type.BrokenAsteroid, enemy.Presenter.Position, 
                    -enemy.Presenter.Transform.right);
                Spawn(IEnemyTypeAdapter.Type.BrokenAsteroid, enemy.Presenter.Position, 
                    enemy.Presenter.Transform.right);
            }
        }

        private IEnemyTypeAdapter.Type GetEnemyType()
        {
            var enemyTypeSettings = ManagerSettings.TypeSettings;
            var totalChanceValue = enemyTypeSettings.Sum(chance => chance.Chance);
            var randomChance = Random.Range(0, totalChanceValue);
            float currentChance = 0;

            foreach (var settings in enemyTypeSettings)
            {
                currentChance += settings.Chance;
                
                if(currentChance >= randomChance)
                    return settings.Type;
            }
            
            return IEnemyTypeAdapter.Type.Asteroid;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public List<EnemiesTypeSettings> TypeSettings { get; private set; }
            [field: SerializeField] public float SpawnDelay { get; private set; }
            [field: SerializeField] public float TeleportCheckerDelay { get; private set; }
            [field: SerializeField] public int StartCount { get; private set; }
            [field: SerializeField] public int MaxCount { get; private set; }
            [field: SerializeField] public float StartVelocity { get; private set; }
            [field: SerializeField] public float Drag { get; private set; }
        }

        [Serializable]
        public class EnemiesTypeSettings
        {
            [field: SerializeField] public IEnemyTypeAdapter.Type Type { get; private set; }
            [field: SerializeField] public float Chance { get; private set; }
            [field: SerializeField] public Material Material { get; private set; }
            [field: SerializeField] public Mesh Mesh { get; private set; }
            [field: SerializeField] public float MinScale { get; private set; }
            [field: SerializeField] public float MaxScale { get; private set; }
            [field: SerializeField] public float VelocityMultiplier { get; private set; }
        }
    }
}