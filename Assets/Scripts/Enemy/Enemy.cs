using Movement;
using Movement.Behaviour;
using Player;
using Presenter;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class Enemy : Core, IFixedTickable, IEnemy
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
            Presenter.MeshRenderer.material = _settings.Material;
            Presenter.MeshFilter.mesh = _settings.Mesh;
        }

        public void SetEnemyType(IEnemy.Type type)
        {
            var velocity = EnemiesManagerData.GetRandomEnemyVelocity() * _settings.StartVelocity;;

            switch (type)
            {
                case IEnemy.Type.Asteroid:
                    _movement.SetMoveBehaviour(new LinearMoveBehaviour(Presenter, velocity));
                    
                    break;
                case IEnemy.Type.Ufo:
                    _movement.SetMoveBehaviour(new FollowingPlayerMoveBehaviour(Presenter,
                        _playerMovementAdapter, velocity.magnitude));
                    
                    break;
            }
        }

        public void FixedTick() => _movement.FixedTick();
    }

    public interface IEnemy
    {
        void SetEnemyType(Type type);
        
        public enum Type
        {
            Asteroid,
            Ufo
        }
    }
}