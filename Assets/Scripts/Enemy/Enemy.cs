using Movement;
using Movement.Behaviour;
using Player;
using Presenter;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class Enemy : Core, IFixedTickable
    {
        private readonly EnemiesManager.Settings _settings;
        private readonly EnemyMovement _movement;

        protected Enemy(Factory.IFactory<IUnitPresenter> factory, EnemiesManager.Settings settings, EnemyMovement movement) 
            : base(factory)
        {
            _settings = settings;
            _movement = movement;
        }

        public override void Init(Transform parent)
        {
            base.Init(parent);
            
            Presenter.IsPlayer = false;
            Presenter.MeshRenderer.material = _settings.Material;
            Presenter.MeshFilter.mesh = _settings.Mesh;
            
            var velocity = EnemiesManagerData.GetRandomEnemyVelocity() * _settings.StartVelocity;;
            _movement.SetMoveBehaviour(new LinearMoveBehaviour(Presenter, velocity));
        }

        public void FixedTick() => _movement.FixedTick();
    }
}