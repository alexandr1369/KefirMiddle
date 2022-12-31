using Factory;
using Movement;
using Movement.Behaviour;
using Player;
using Presenter;
using UnityEngine;

namespace Enemy
{
    public class Enemy : Core
    {
        private readonly EnemiesManager.Settings _settings;
        private readonly ICoreMovement _movement;

        protected Enemy(IFactory<IUnitPresenter> factory, EnemiesManager.Settings settings, ICoreMovement movement) 
            : base(factory)
        {
            Debug.Log("Enemy is enemy movement: " + (movement is PlayerMovement));
            
            _settings = settings;
            _movement = movement;
        }

        public override void Init(Transform parent)
        {
            base.Init(parent);
            
            Presenter.IsPlayer = false;
            Presenter.MeshRenderer.material = _settings.Material;
            Presenter.MeshFilter.mesh = _settings.Mesh;
            
            Debug.Log("Initializing movement: " + (Presenter != null) + " " + (_movement != null));
            var velocity = EnemiesManagerData.GetRandomEnemyVelocity() * _settings.StartVelocity;;
            _movement.SetMoveBehaviour(new LinearMoveBehaviour(Presenter, velocity));
        }
    }
}