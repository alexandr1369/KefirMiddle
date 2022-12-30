using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletsService : IBulletsService
    {
        public event Action<Bullet> OnSpawned;
        
        public List<Bullet> Bullets { get; } = new();
        public Settings ServiceSettings { get; }

        private readonly Utils.IFactory<Bullet> _factory;

        private BulletsService(Utils.IFactory<Bullet> factory, Settings settings)
        {
            _factory = factory;
            ServiceSettings = settings;
        }

        public void SpawnBulletAt(Vector3 position, Vector3 direction)
        {
            var bullet = _factory.Create();
            bullet.Init(null);
            bullet.Presenter.OnDestroyed += () => OnPresenterDestroyed(bullet);
            bullet.Presenter.Position = position;
            bullet.Presenter.Velocity = direction * ServiceSettings.StartVelocity;
            bullet.Presenter.LocalScale = ServiceSettings.LocalScale;
            bullet.Presenter.Drag = ServiceSettings.Drag;
            Bullets.Add(bullet);
            OnSpawned?.Invoke(bullet);
        }

        private void OnPresenterDestroyed(Bullet bullet) => Bullets.Remove(bullet);

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Material Material { get; private set; }
            [field: SerializeField] public Mesh Mesh { get; private set; }
            [field: SerializeField] public Vector3 LocalScale { get; private set; }
            [field: SerializeField] public float TeleportCheckerDelay { get; private set; }
            [field: SerializeField] public float LifeTimeDuration { get; private set; }
            [field: SerializeField] public float StartVelocity { get; private set; }
            [field: SerializeField] public float Drag { get; private set; }
        }
    }
}