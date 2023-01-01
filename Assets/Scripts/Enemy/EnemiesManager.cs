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
    public class EnemiesManager : ITickable, IEnemiesManager
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

        public void Tick()
        {
            Enemies.ForEach(enemy => enemy.FixedTick());
            
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
            var type = GetEnemyType();
            var typeSettings = ManagerSettings.TypeSettings.Find(settings => settings.Type == type);
            enemy.Init(null);
            ((IEnemyTypeAdapter)enemy).SetEnemyType(type);
            enemy.Presenter.OnDestroyed += () => OnPresenterDestroyed(enemy);
            enemy.Presenter.LocalScale = 
                EnemiesManagerData.GetRandomLocalScale(typeSettings.MinScale, typeSettings.MaxScale);
            enemy.Presenter.Position =
                EnemiesManagerData.GetRandomEnemyPosition(enemy.Presenter.LocalScale.x, _service);
            enemy.Presenter.Drag = ManagerSettings.Drag;
            Enemies.Add(enemy);
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

        private void OnPresenterDestroyed(Enemy enemy) => Enemies.Remove(enemy);

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
        }
    }
}