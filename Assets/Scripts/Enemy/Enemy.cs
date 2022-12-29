using Presenter;
using UnityEngine;

namespace Enemy
{
    public class Enemy : Player.Player
    {
        private readonly EnemiesManager.Settings _settings;

        protected Enemy(Utils.IFactory<IUnitPresenter> factory, EnemiesManager.Settings settings) 
            : base(factory)
        {
            _settings = settings;
        }

        public override void Init(Transform parent)
        {
            base.Init(parent);
            
            Presenter.IsPlayer = false;
            Presenter.MeshRenderer.material = _settings.Material;
            Presenter.MeshFilter.mesh = _settings.Mesh;
        }
    }
}