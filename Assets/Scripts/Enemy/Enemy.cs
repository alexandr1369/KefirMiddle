using Movement;
using Movement.Behaviour;
using Player;
using Presenter;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class Enemy : Core, IFixedTickable, IEnemyTypeAdapter
    {
        public IEnemyTypeAdapter.Type EnemyType { get; private set; }
        
        private readonly EnemiesManager.Settings _settings;
        private readonly EnemyMovement _movement;
        private readonly IPlayerMovementAdapter _playerMovementAdapter;

        protected Enemy(Factory.IFactory<IUnitPresenter> factory, EnemiesManager.Settings settings,
            EnemyMovement movement, IPlayerMovementAdapter playerMovementAdapter) 
            : base(factory)
        {
            _settings = settings;
            _movement = movement;
            _playerMovementAdapter = playerMovementAdapter;
        }

        public override void Init(Transform parent)
        {
            base.Init(parent);
            
            Presenter.IsPlayer = false;
        }

        public void SetEnemyType(IEnemyTypeAdapter.Type type, Vector3? velocity = null)
        {
            EnemyType = type;
            var enemy = _settings.TypeSettings.Find(t => t.Type == type);
            var randomVelocity = EnemiesManagerData.GetRandomEnemyVelocity() * _settings.StartVelocity * enemy.VelocityMultiplier;
            Presenter.MeshRenderer.material = enemy.Material;
            Presenter.MeshFilter.mesh = enemy.Mesh;

            switch (type)
            {
                case IEnemyTypeAdapter.Type.Asteroid:
                case IEnemyTypeAdapter.Type.BrokenAsteroid:
                    _movement.SetMoveBehaviour(new LinearMoveBehaviour(Presenter, randomVelocity));
                    
                    break;
                case IEnemyTypeAdapter.Type.Ufo:
                    _movement.SetMoveBehaviour(new FollowingPlayerMoveBehaviour(Presenter,
                        _playerMovementAdapter, randomVelocity.magnitude));
                    
                    break;
            }
        }

        public void FixedTick() => _movement.FixedTick();
    }
}