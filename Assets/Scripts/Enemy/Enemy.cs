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

        public void SetEnemyType(IEnemyTypeAdapter.Type type)
        {
            var enemy = _settings.TypeSettings.Find(t => t.Type == type);
            var velocity = EnemiesManagerData.GetRandomEnemyVelocity() * _settings.StartVelocity;;
            Presenter.MeshRenderer.material = enemy.Material;
            Presenter.MeshFilter.mesh = enemy.Mesh;

            switch (type)
            {
                case IEnemyTypeAdapter.Type.Asteroid:
                    _movement.SetMoveBehaviour(new LinearMoveBehaviour(Presenter, velocity));
                    
                    break;
                case IEnemyTypeAdapter.Type.Ufo:
                    _movement.SetMoveBehaviour(new FollowingPlayerMoveBehaviour(Presenter,
                        _playerMovementAdapter, velocity.magnitude));
                    
                    break;
            }
        }

        public void FixedTick() => _movement.FixedTick();
    }
}